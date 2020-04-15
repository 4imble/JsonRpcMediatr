using _4imble.JsonRpcMediatr;
using _4imble.JsonRpcMediatr.RequestsResponses;
using _4imble.JsonRpcMediatr.Specs.Helpers;

namespace _4imble.Mediatr.JsonRpc.Specs.TestClasses
{
    public class TestJsonRpcLogger : IJsonRpcRequestLogger
    {
        public void Log(JsonRpcRequest request)
        {
            TestContext.Remember("Log", request);
        }
    }
}
