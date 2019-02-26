using System.Threading;
using System.Threading.Tasks;
using MediatR;

// ReSharper disable once CheckNamespace
namespace _4imble.Mediatr.JsonRpc.Specs.TestRequests
{
    public class SubtractRequest : IRequest<int>
    {
        public int Left { get; set; }
        public int Right { get; set; }

        internal class Handler : IRequestHandler<SubtractRequest, int>
        {
            public Task<int> Handle(SubtractRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(request.Left - request.Right);
            }
        }
    }
}
