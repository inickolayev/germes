using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Results;
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
        private readonly AccountantService _accountantService;
        private readonly List<Session> _sessions = new List<Session>();

        public List<IBotPlugin> DefaultPlugins => new List<IBotPlugin>
        {
            new AccountantPlugin(_accountantService),
        };

        public SessionService(IUserService userService, AccountantService accountantService)
        {
            _userService = userService;
            _accountantService = accountantService;
        }

        public async Task<OperationResult<Session>> AddSessionAsync(string chatId, CancellationToken token)
        {
            var session = _sessions.SingleOrDefault(s => s.User.ChatId == chatId);
            if (session != null)
                return new OperationResult<Session>(new Exception("Session already exists"));
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
                User = user,
                Plugins = DefaultPlugins
            };
            _sessions.Add(session);
            return new OperationResult<Session>(session);
        }

        public async Task<OperationResult<Session>> GetSessionAsync(string chatId, CancellationToken token)
            => new OperationResult<Session>(_sessions.SingleOrDefault(user => user.User.ChatId == chatId));
    }
}
