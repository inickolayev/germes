using Germes.Domain.Data;
using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using Germes.Domain.Data.Results.Errors;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Implementations.Services
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IUserService _userService;
        private readonly ISessionManager _sessionManager;
        private static readonly List<Session> _sessions = new List<Session>();

        public SessionRepository(IUserService userService, ISessionManager sessionManager)
        {
            _userService = userService;
            _sessionManager = sessionManager;
        }

        public async Task<OperationResult<Session>> AddSessionAsync(string chatId, CancellationToken token)
        {
            var session = _sessions.SingleOrDefault(s => s.User.ChatId == chatId);
            if (session != null)
                return new OperationResult<Session>(SessionErrors.SessionNotExist(chatId));
            var userRes = await _userService.GetUserAsync(chatId, token);
            if (!userRes.IsSuccess)
                return userRes.To<Session>();
            var user = userRes.Result;
            if (user == null)
            {
                var userAddRes = await _userService.AddUserAsync(new UserModel { ChatId = chatId }, token);
                if (!userAddRes.IsSuccess)
                    return userAddRes.To<Session>();
                user = userAddRes.Result;
            }

            session = new Session
            {
                User = user
            };
            _sessions.Add(session);
            return new OperationResult<Session>(session);
        }

        public async Task<OperationResult<Session>> GetSessionAsync(string chatId, CancellationToken token)
            => new OperationResult<Session>(_sessions.SingleOrDefault(user => user.User.ChatId == chatId));
    }
}
