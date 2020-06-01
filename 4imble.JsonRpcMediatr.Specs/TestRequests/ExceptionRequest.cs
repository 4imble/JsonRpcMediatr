using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace _4imble.JsonRpcMediatr.Specs.TestRequests
{
    public class ExceptionRequest : IRequest<string>
    {
        internal class Handler : IRequestHandler<ExceptionRequest, string>
        {
            public Task<string> Handle(ExceptionRequest request, CancellationToken cancellationToken)
            {
                throw new ApplicationException("Oh no there's an unhandled exception!");
            }
        }
    }
}
