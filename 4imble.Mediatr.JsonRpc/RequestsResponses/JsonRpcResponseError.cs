namespace _4imble.Mediatr.JsonRpc.RequestsResponses
{
    public class JsonRpcResponseError: JsonRpcResponse
    {
        public JsonRpcError Error { get; set; }
    }
}
