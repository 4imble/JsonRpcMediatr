using System.Security.Claims;
using System.Threading.Tasks;
using _4imble.JsonRpcMediatr.RequestsResponses;

namespace _4imble.JsonRpcMediatr
{
    public interface IJsonRpcRequestHandler
    {
        Task<JsonRpcResponse> HandleJsonRpcRequest(ClaimsPrincipal user, string jsonRpcRequest);
    }
}