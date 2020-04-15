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

## Register with IoC
```
    public static void RegisterJsonRpcHandler(this Container container)
    {
        // MediatR Request
        var knownType = typeof(PingRequest);

        // Get all requests for application
        var registrations =
            from type in knownType.Assembly.GetExportedTypes()
            where type.Namespace == knownType.Namespace
            where type.GetInterfaces().Any(x => x == typeof(IBaseRequest))
            select type;
        
        // Register handler with requests
        container.RegisterSingleton<IJsonRpcRequestHandler>(
            () => new JsonRpcRequestHandler(container.GetInstance<IMediator>(), registrations, new NoOpJsonRpcRequestLogger()));
    }
```

## New in 1.0.3
### Logging
You can no get it to log the requests that get handled.
Implement the IJsonRpcRequestLogger interface and pass it to IoC instead of the NoOpJsonRpcRequestLogger

## Form a valid RPC request 
### Example
`{"jsonrpc": "2.0", "method": "ping", "id": "1"}`

### Expect a response
`{"jsonrpc": "2.0", "result": "Pong", "id": "1"}`

where the following mediatr request exists on the server

```
public class PingRequest : IRequest<string>
    {
        internal class Handler : IRequestHandler<PingRequest, string>
        {
            public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult("Pong");
            }
        }
    }
```

## Further Reading
See the specs / features for further examples

and the official spec
https://www.jsonrpc.org/specification#examples


### Here's a typescript helper method (server.ts) for convenience
Something like this can help you form the correct RPC to send.
```

//ServerTs
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
  Used like so :

```
    let stuffs = await this.server.jsonRpc("GetAllStuff");
    // OR  
    let model = { Id: 21 };
    let specificStuff = this.server.jsonRpc("GetSpecificStuffById", { "Model": model });
```
