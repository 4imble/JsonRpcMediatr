namespace _4imble.Mediatr.JsonRpc.RequestsResponses
{
    public class JsonRpcError
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}