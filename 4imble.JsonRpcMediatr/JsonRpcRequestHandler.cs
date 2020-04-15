using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using _4imble.JsonRpcMediatr.Attributes;
using _4imble.JsonRpcMediatr.RequestsResponses;
using System.Diagnostics;

namespace _4imble.JsonRpcMediatr
{
    public class JsonRpcRequestHandler : IJsonRpcRequestHandler
    {
        readonly IMediator mediator;
        readonly IEnumerable<Type> requests;
        readonly IJsonRpcRequestLogger requestLogger;

        JsonRpcError PARSE_ERROR = new JsonRpcError { Code = -32700, Message = "Parse error" };
        JsonRpcError INVALID_REQUEST = new JsonRpcError { Code = -32600, Message = "Invalid Request" };
        JsonRpcError METHOD_NOT_FOUND = new JsonRpcError { Code = -32601, Message = "Method not found" };
        JsonRpcError USER_NOT_AUTHORIZED = new JsonRpcError { Code = -00001, Message = "User not authorized" };

        public JsonRpcRequestHandler(IMediator mediator, IEnumerable<Type> requests, IJsonRpcRequestLogger requestLogger)
        {
            this.mediator = mediator;
            this.requests = requests;
            this.requestLogger = requestLogger;
        }

        public async Task<JsonRpcResponse> HandleJsonRpcRequest(ClaimsPrincipal user, string jsonRpcRequest)
        {
            JsonRpcRequest request;

            try
            {
                request = JsonConvert.DeserializeObject<JsonRpcRequest>(jsonRpcRequest);
            }
            catch (JsonException)
            {
                return new JsonRpcResponseError { Error = PARSE_ERROR };
            }

            if (request.JsonRpc != JsonRpcConstants.JSON_RPC_VERSION)
                return new JsonRpcResponseError { Error = INVALID_REQUEST, Id = request.Id };

            if (request.Method.All(char.IsDigit))
                return new JsonRpcResponseError { Error = INVALID_REQUEST, Id = request.Id };

            var mediatorRequestType = requests.FirstOrDefault(x => x.Name.ToUpper() == $"{request.Method.ToUpper()}REQUEST");

            if (mediatorRequestType == null)
            {
                return new JsonRpcResponseError { Error = METHOD_NOT_FOUND, Id = request.Id };
            }

            // Todo: Maybe reuse this for roles later.
            if (mediatorRequestType.GetCustomAttribute<SecuredAttribute>() != null)
            {
                if (!user.Identity.IsAuthenticated)
                    return new JsonRpcResponseError { Error = USER_NOT_AUTHORIZED, Id = request.Id };
            }

            dynamic mediatorRequest = Activator.CreateInstance(mediatorRequestType);

            if (request.Params != null)
                JsonConvert.PopulateObject(request.Params.ToString(), mediatorRequest);

            var sw = Stopwatch.StartNew();
            var result = await mediator.Send(mediatorRequest);
            sw.Stop();

            request.ExecutionTime = (int)sw.ElapsedMilliseconds;

            try
            {
                requestLogger.Log(request);
            }
            catch (Exception){}

            return await Task.FromResult(new JsonRpcResponseSuccess { Result = result, Id = request.Id });
        }
    }
}
