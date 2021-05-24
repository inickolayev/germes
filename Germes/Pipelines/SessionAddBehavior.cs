using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Pipelines
{
    public class SessionAddBehavior : IPipelineBehavior<RequestNewMessage, OperationResult<BotResult>>
    {
        private readonly ISessionService _sessionService;
        private readonly ISessionManager _sessionManager;
        private readonly IUserService _userService;

        public SessionAddBehavior(ISessionService sessionService, ISessionManager sessionManager, IUserService userService)
        {
            _sessionService = sessionService;
            _sessionManager = sessionManager;
            _userService = userService;
        }

        public async Task<OperationResult<BotResult>> Handle(RequestNewMessage request, CancellationToken token, RequestHandlerDelegate<OperationResult<BotResult>> next)
        {
            var message = request.Message;
            var chatId = message.ChatId;
            var sessionRes = await _sessionService.GetSessionAsync(chatId, token);
            if (!sessionRes.IsSuccess)
                return sessionRes.To<BotResult>();
            var session = sessionRes.Result;
            if (session == null)
            {
                var userAddRes = await _userService.AddUserAsync(new UserModel
                {
                    Id = new Guid().ToString(),
                    Name = "Some"
                });
                if (!userAddRes.IsSuccess)
                    return userAddRes.To<BotResult>();
                var user = userAddRes.Result;
                var sessionAddRes = await _sessionService.AddSessionAsync(chatId, user.Id, token);
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
