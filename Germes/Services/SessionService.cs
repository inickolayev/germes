using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Results;
using Germes.Data.Results.Errors;
using Germes.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Germes.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUserService _userService;
        private readonly ISessionManager _sessionManager;
        private static readonly List<SessionModel> _sessions = new List<SessionModel>();

        public SessionService(IUserService userService, ISessionManager sessionManager)
        {
            _userService = userService;
            _sessionManager = sessionManager;
        }

        public async Task<OperationResult<SessionModel>> AddSessionAsync(string chatId, string userId, CancellationToken token)
        {
            var session = _sessions.SingleOrDefault(s => s.ChatId == chatId);
            if (session != null)
                return new OperationResult<SessionModel>(SessionErrors.SessionAlreadyExist(chatId));
            var userRes = await _userService.GetUserAsync(userId, token);
            if (!userRes.IsSuccess || userRes.Result == null)
                return userRes.To<SessionModel>();
            var user = userRes.Result;

            session = new SessionModel
            {
                User = user
            };
            _sessions.Add(session);
            return new OperationResult<SessionModel>(session);
        }

        public async Task<OperationResult> AddSessionAsync(SessionModel session, CancellationToken token)
        {
            if (_sessions.Any(s => s.User == session.User && s.ChatId == session.ChatId))
                return new OperationResult(SessionErrors.SessionAlreadyExist(session.ChatId));
            _sessions.Add(session);
            return new OperationResult();
        }

        public async Task<OperationResult<SessionModel>> GetSessionAsync(string chatId, CancellationToken token)
            => new OperationResult<SessionModel>(_sessions.SingleOrDefault(s => s.ChatId == chatId));
    }
}
