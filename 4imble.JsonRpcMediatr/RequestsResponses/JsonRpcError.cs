﻿namespace _4imble.JsonRpcMediatr.RequestsResponses
{
    public class JsonRpcError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}