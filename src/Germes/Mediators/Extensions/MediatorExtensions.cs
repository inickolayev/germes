﻿using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;

namespace Germes.Mediators.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task<OperationResult<TResult>> SendSafe<TRequest, TResult>(this IMediator mediator, TRequest request, CancellationToken token = default)
            where TRequest : IRequest<OperationResult<TResult>>
        {
            OperationResult<TResult> result;
            try
            {
                result = await mediator.Send(request, token);
            }
            catch (Exception e)
            {
                result = new OperationResult<TResult>(new InternalError("Общая внутреняя ошибка", e));
            }

            return result;
        }

        public static async Task<OperationResult> SendSafe<TRequest>(this IMediator mediator, TRequest request, CancellationToken token = default)
            where TRequest : IRequest<OperationResult>
        {
            OperationResult result;
            try
            {
                result = await mediator.Send(request, token);
            }
            catch (Exception e)
            {
                result = new OperationResult(new InternalError("Общая внутреняя ошибка", e));
            }

            return result;
        }
    }
}
