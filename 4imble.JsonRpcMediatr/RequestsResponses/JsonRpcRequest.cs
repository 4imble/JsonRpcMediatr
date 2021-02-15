using Newtonsoft.Json.Linq;

namespace _4imble.JsonRpcMediatr.RequestsResponses
{
    public class JsonRpcRequest
    {
        public string JsonRpc { get; set; }
        public string Method { get; set; }
        public JObject Params { get; set; }
        public string Id { get; set; }
        public int ExecutionTime { get; set; } = -1;
    }
}
