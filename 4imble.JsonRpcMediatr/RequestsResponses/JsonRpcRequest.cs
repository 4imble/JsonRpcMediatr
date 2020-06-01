using Newtonsoft.Json.Linq;
using System;

namespace _4imble.JsonRpcMediatr.RequestsResponses
{
    public class JsonRpcRequest
    {
        public string JsonRpc { get; set; }
        public string Method { get; set; }
        public JObject Params { get; set; }
        public string Id { get; set; }
        public TimeSpan ExecutionTime { get; set; }
    }
}
