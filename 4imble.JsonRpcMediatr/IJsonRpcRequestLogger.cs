using _4imble.JsonRpcMediatr.RequestsResponses;

namespace _4imble.JsonRpcMediatr
{
    public interface IJsonRpcRequestLogger
    {
        void Log(JsonRpcRequest request);
    }
}
