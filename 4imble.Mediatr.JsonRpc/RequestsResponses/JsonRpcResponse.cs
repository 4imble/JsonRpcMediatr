namespace _4imble.Mediatr.JsonRpc.RequestsResponses
{
    public class JsonRpcResponse
    {
        public string JsonRpc => JsonRpcConstants.JSON_RPC_VERSION;

        public string Id { get; set; }
    }
}
