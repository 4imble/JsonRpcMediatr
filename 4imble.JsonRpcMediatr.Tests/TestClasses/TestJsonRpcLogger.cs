using _4imble.JsonRpcMediatr;
using _4imble.JsonRpcMediatr.RequestsResponses;
using _4imble.JsonRpcMediatr.Tests.Helpers;
using System;

namespace _4imble.Mediatr.JsonRpc.Specs.TestClasses
{
    public class TestJsonRpcLogger : IJsonRpcRequestLogger
    {
        public void Log(JsonRpcRequest request, Type mediatorRequestType)
        {
            TestContext.Remember("LogRequest", request);
            TestContext.Remember("LogRequestType", mediatorRequestType);
        }
    }
}
