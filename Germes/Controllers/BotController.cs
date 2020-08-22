using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Germes.Controllers
{
    public class BotController : BaseApiController
    {
        private readonly ILogger<BotController> _logger;
        private readonly TelegramBotClient _client;

        public BotController(ILogger<BotController> logger, TelegramBotClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpPost]
        public async Task HandleAsync(Update update, CancellationToken token)
        {
            if (update == null)
                throw new Exception("Update is null");
            if (update.Type == UpdateType.Message)
                await HandleMessageAsync(update, token);
            else if (update.Type == UpdateType.EditedMessage)
                await HandleEditedMessageAsync(update, token);
        }

        private async Task HandleMessageAsync(Update update, CancellationToken token)
        {
            await _client.SendTextMessageAsync(update.Message.Chat.Id, $"Принял! {update.Message.Text}", cancellationToken: token);
        }


        private async Task HandleEditedMessageAsync(Update update, CancellationToken token)
        {
            await _client.SendTextMessageAsync(update.EditedMessage.Chat.Id, "Опа, да тут сообщения правят -_-", cancellationToken: token);
        }
    }
}
