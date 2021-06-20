using Germes.Domain.Data;
using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using Germes.Domain.Data.Results.Errors;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Domain.Repositories;
using Germes.Domain.Services;
using Germes.Implementations.Services;

namespace Germes.Implementations.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionManager _sessionManager;
        private static readonly List<Session> _sessions = new List<Session>();

        public SessionService(IUserRepository userRepository, ISessionManager sessionManager)
        {
            _userRepository = userRepository;
            _sessionManager = sessionManager;
        }

        public async Task<OperationResult<Session>> AddSessionAsync(string chatId, CancellationToken token)
        {
            var session = _sessions.SingleOrDefault(s => s.User.ChatId == chatId);
            if (session != null)
                return new OperationResult<Session>(SessionErrors.SessionNotExist(chatId));
            var user = await _userRepository.GetUserAsync(chatId, token)
                       ?? await _userRepository.AddUserAsync(new User { ChatId = chatId }, token);

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