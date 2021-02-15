using _4imble.JsonRpcMediatr.RequestsResponses;
using System;

namespace _4imble.JsonRpcMediatr
{
    public interface IJsonRpcRequestLogger
    {
        void Log(JsonRpcRequest request, Type mediatorRequestType);
    }
}
