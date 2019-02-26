using System;
using System.Security.Claims;
using MediatR;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using _4imble.Mediatr.JsonRpc.Specs.Configuration;
using _4imble.Mediatr.JsonRpc.Specs.Helpers;
using _4imble.Mediatr.JsonRpc.Specs.TestRequests;

namespace _4imble.Mediatr.JsonRpc.Specs.Steps
{
    [Binding]
    public class WhenSteps
    {
        readonly IJsonRpcRequestHandler jsonRpcRequestHandler;
        readonly IMediator mediator;

        public WhenSteps()
        {
            jsonRpcRequestHandler = ApplicationContext.Resolve<IJsonRpcRequestHandler>();
            mediator = ApplicationContext.Resolve<IMediator>();
        }

        [When(@"I send the request with identifier '(.*)'")]
        public void WhenISendTheRequestWithIdentifier(string identifier)
        {
            var user = TestContext.Recall<ClaimsPrincipal>("CurrentUser");

            var jsonRpcRequest = TestContext.Recall<string>(identifier);
            var response = jsonRpcRequestHandler.HandleJsonRpcRequest(user, jsonRpcRequest).Result;

            TestContext.Remember("response", response);
        }

        [When(@"I send a '(.*)' request")]
        public void GivenISendARequest(string requestName)
        {
            var knownType = typeof(PingRequest);
            var mediatorRequestType =
                knownType.Assembly.GetType($"{knownType.Namespace}.{requestName}Request", false, true);

            dynamic mediatorRequest = Activator.CreateInstance(mediatorRequestType);

            var result = mediator.Send(mediatorRequest).Result;

            TestContext.Remember("Result", result);
        }


        [When(@"I send a '(.*)' request with parameters")]
        public void GivenISendARequestWithParameters(string requestName, dynamic parameterData)
        {
            var knownType = typeof(PingRequest);
            var mediatorRequestType =
                knownType.Assembly.GetType($"{knownType.Namespace}.{requestName}Request", false, true);

            dynamic mediatorRequest = Activator.CreateInstance(mediatorRequestType);

            var parameters = PropertyAccessHelper.TryGetValue<string>(() => parameterData.Parameters);
            if (!string.IsNullOrEmpty(parameters))
                JsonConvert.PopulateObject(parameters, mediatorRequest);

            var result = mediator.Send(mediatorRequest).Result;

            TestContext.Remember("Result", result);
        }
    }
}
