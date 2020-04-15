using System.Threading;
using System.Threading.Tasks;
using MediatR;
using _4imble.JsonRpcMediatr.Attributes;

// ReSharper disable once CheckNamespace
namespace _4imble.JsonRpcMediatr.TestRequests
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
