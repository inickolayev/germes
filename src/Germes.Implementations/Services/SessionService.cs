using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Repositories;
using Germes.Abstractions.Services;
using Germes.Accountant.Domain.Models;

namespace Germes.Implementations.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUserRepository _userRepository;
        private static readonly List<Session> _sessions = new List<Session>();

        public SessionService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<Session>> AddSessionAsync(string chatId, CancellationToken token)
        {
            var session = _sessions.SingleOrDefault(s => s.User.ChatId == chatId);
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