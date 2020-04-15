using System.Threading;
using System.Threading.Tasks;
using MediatR;

// ReSharper disable once CheckNamespace
namespace _4imble.JsonRpcMediatr.Specs.TestRequests
{
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
}
