using Newtonsoft.Json.Linq;

namespace _4imble.Mediatr.JsonRpc.RequestsResponses
{
    public class JsonRpcRequest
    {
        public string JsonRpc { get; set; }
        public string Method { get; set; }
        public JObject Params { get; set; }
        public string Id { get; set; }
    }
}
