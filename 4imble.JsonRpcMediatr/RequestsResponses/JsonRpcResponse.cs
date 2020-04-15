namespace _4imble.JsonRpcMediatr.RequestsResponses
{
    public class JsonRpcResponse
    {
        public string JsonRpc => JsonRpcConstants.JSON_RPC_VERSION;

        public string Id { get; set; }
    }
}
