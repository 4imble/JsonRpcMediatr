## How to use

Create a controller that passes the request to the handler

```
[Produces("application/json")]
    public class JsonRpcController : Controller
    {
        readonly IJsonRpcRequestHandler jsonRpcRequestHandler;

        public JsonRpcController(IJsonRpcRequestHandler jsonRpcRequestHandler)
        {
            this.jsonRpcRequestHandler = jsonRpcRequestHandler;
        }

        [HttpPost]
        [Route("api/JsonRpc")]
        public async Task<dynamic> Execute([FromBody]JsonRpcModel request)
        {
            return await jsonRpcRequestHandler.HandleJsonRpcRequest(User, request.Content);
        }
    }
```

## Reading
See the specs / features for further examples

and the official spec
https://www.jsonrpc.org/specification#examples

## Form a valid RPC request 
### Example
`{"jsonrpc": "2.0", "method": "ping", "id": "1"}`

### Expect a response
`{"jsonrpc": "2.0", "result": "Pong", "id": "1"}`

### Here's a typescript helper method for convenience)
```
async jsonRpc(method, params?): Promise<any> {
    var guid = Guid.NewGuid();

    var json = JSON.stringify({
      jsonrpc: '2.0',
      method: method,
      params: params,
      id: guid
    });

    try {
      var data = await this.client.post("api/JsonRpc/", { content: json });
      var response = JSON.parse(data.response);
      if(response.error)
      {
        throw new Error(response.error.message);
      }
    }
    catch (exception) {
      var message = "Oh no, something has gone wrong on the server";
      if(typeof exception === 'string' || exception instanceof String)
        message += ` (${exception})`;
      console.log(message);
    }

    return response.result;
  }
  ```