using System.Threading;
using System.Threading.Tasks;
using MediatR;
using _4imble.Mediatr.JsonRpc.Attributes;

// ReSharper disable once CheckNamespace
namespace _4imble.Mediatr.JsonRpc.Specs.TestRequests
{
    [Secured]
    public class SecuredRequest : IRequest
    {
        internal class Handler : IRequestHandler<SecuredRequest>
        {
            public Task<Unit> Handle(SecuredRequest request, CancellationToken cancellationToken)
            {
                return Unit.Task;
            }
        }
    }
}
