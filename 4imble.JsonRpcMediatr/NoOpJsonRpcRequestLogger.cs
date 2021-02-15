using _4imble.JsonRpcMediatr.RequestsResponses;
using System;

namespace _4imble.JsonRpcMediatr
{
    public class NoOpJsonRpcRequestLogger : IJsonRpcRequestLogger
    {
        public void Log(JsonRpcRequest request, Type mediatorRequestType)
        {
        }
    }
}
