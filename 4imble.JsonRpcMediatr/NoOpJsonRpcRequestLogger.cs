using _4imble.JsonRpcMediatr.RequestsResponses;

namespace _4imble.JsonRpcMediatr
{
    public class NoOpJsonRpcRequestLogger : IJsonRpcRequestLogger
    {
        public void Log(JsonRpcRequest request)
        {
            
        }
    }
}
