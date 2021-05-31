using Germes.Domain.Data.Results;
using Germes.Implementations.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Germes.Data;
using Germes.Mediators.Requests;

namespace Germes.Mediators.Pipelines
{
    public class SessionAddBehavior : IPipelineBehavior<RequestNewMessage, OperationResult<BotResult>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ISessionManager _sessionManager;

        public SessionAddBehavior(ISessionRepository sessionRepository, ISessionManager sessionManager)
        {
            _sessionRepository = sessionRepository;
            _sessionManager = sessionManager;
        }

        //public Task<TResponse> Handle(TRequest request, CancellationToken token, RequestHandlerDelegate<TResponse> next)
        //{
        //    if (request is RequestNewMessage req)
        //        return InternalHandle(req, token, next as RequestHandlerDelegate<OperationResult<BotResult>>) as Task<TResponse>;

        //    return next();
        //}

        public async Task<OperationResult<BotResult>> Handle(RequestNewMessage request, CancellationToken token, RequestHandlerDelegate<OperationResult<BotResult>> next)
        {
            var message = request.Message;
            var chatId = message.ChatId;
            var sessionRes = await _sessionRepository.GetSessionAsync(chatId, token);
            if (!sessionRes.IsSuccess)
                return sessionRes.To<BotResult>();
            var session = sessionRes.Result;
            if (session == null)
            {
                var sessionAddRes = await _sessionRepository.AddSessionAsync(chatId, token);
                if (!sessionAddRes.IsSuccess)
                    return sessionAddRes.To<BotResult>();
                session = sessionAddRes.Result;
            }
            var setRes = _sessionManager.SetCurrentSession(session);
            if (!setRes.IsSuccess)
                return setRes.To<BotResult>();

            var response = await next();

            return response;
        }
    }
}
