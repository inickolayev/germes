using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Results;
using Germes.Data.Results.Errors;
using Germes.Services;
using Germes.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Germes.Test.Mocks
{
    public class SessionServiceMock : ISessionService, IDisposable
    {
        private readonly IUserService _userService;
        private readonly ISessionManager _sessionManager;
        
        public static readonly List<Session> Sessions = new List<Session>();

        public SessionServiceMock(IUserService userService, ISessionManager sessionManager)
        {
            _userService = userService;
            _sessionManager = sessionManager;
        }

        public async Task<OperationResult<Session>> AddSessionAsync(string chatId, CancellationToken token)
        {
            var session = Sessions.SingleOrDefault(s => s.User.ChatId == chatId);
            if (session != null)
                return new OperationResult<Session>(SessionErrors.SessionAlreadyExist(chatId));
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
            Sessions.Add(session);
            return new OperationResult<Session>(session);
        }

        public async Task<OperationResult<Session>> GetSessionAsync(string chatId, CancellationToken token)
            => new OperationResult<Session>(Sessions.SingleOrDefault(user => user.User.ChatId == chatId));

        public void Dispose()
        {
            Sessions.Clear();
        }
    }
}
