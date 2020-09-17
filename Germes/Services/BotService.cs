using Germes.Data;
using Germes.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Services
{
    public class BotService : IBotService
    {
        private readonly ISessionService _sessionService;

        public BotService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<OperationResult<BotResult>> HandleNewMessageAsync(BotMessage message, CancellationToken token)
        {
            var chatId = message.ChatId;
            var sessionRes = await _sessionService.GetSessionAsync(chatId, token);
            if (!sessionRes.IsSuccess)
                return sessionRes.To<BotResult>();
            var session = sessionRes.Result;
            if (session == null)
            {
                var sessionAddRes = await _sessionService.AddSessionAsync(chatId, token);
                if (!sessionAddRes.IsSuccess)
                    return sessionAddRes.To<BotResult>();
                session = sessionAddRes.Result;
            }
            foreach(var plugin in session.Plugins)
            {
                var checkRes = await plugin.CheckAsync(message, token);
                if (!checkRes.IsSuccess)
                    return checkRes.To<BotResult>();
                if (checkRes.Result)
                {
                    var result = await plugin.HandleAsync(session, message, token);
                    return result;
                }
            }
            return new OperationResult<BotResult>(new BotResult
            {
                Text = "Ни одного активного плагина не найдено!"
            });
        }
    }
}
