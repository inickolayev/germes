using Germes.Data.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<OperationResult<TResult>> SendSafe<TRequest, TResult>(this IMediator mediator, TRequest request, CancellationToken token = default)
            where TRequest : IRequest<TResult>
        {
            OperationResult<TResult> result;
            try
            {
                var ans = await mediator.Send(request, token);
                result = new OperationResult<TResult>(ans);
            }
            catch(Exception e)
            {
                result = new OperationResult<TResult>(e);
            }

            return result;
        }
    }
}
