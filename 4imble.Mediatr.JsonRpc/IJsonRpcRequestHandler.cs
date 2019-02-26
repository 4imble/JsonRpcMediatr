using System.Security.Claims;
using System.Threading.Tasks;
using _4imble.Mediatr.JsonRpc.RequestsResponses;

namespace _4imble.Mediatr.JsonRpc
{
    public interface IJsonRpcRequestHandler
    {
        Task<JsonRpcResponse> HandleJsonRpcRequest(ClaimsPrincipal user, string jsonRpcRequest);
    }
}