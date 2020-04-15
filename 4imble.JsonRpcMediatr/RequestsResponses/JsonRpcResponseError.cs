namespace _4imble.JsonRpcMediatr.RequestsResponses
{
    public class JsonRpcResponseError : JsonRpcResponse
    {
        public JsonRpcError Error { get; set; }
    }
}
