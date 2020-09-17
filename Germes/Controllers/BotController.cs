using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Data;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Extensions;
using MediatR;
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
        private readonly IMediator _mediator;

        public BotController(ILogger<BotController> logger, TelegramBotClient client, IMediator mediator)
        {
            _logger = logger;
            _client = client;
            _mediator = mediator;
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
            var mess = new BotMessage
            {
                ChatId = update.Message.Chat.Id.ToString(),
                Text = update.Message.Text
            };
            var chatId = update.Message.Chat.Id;
            var res = await _mediator.SendSafe<RequestNewMessage, BotResult>(new RequestNewMessage { Message = mess }, token);
            if (res.IsSuccess)
                await _client.SendTextMessageAsync(chatId, res.Result.Text, cancellationToken: token);
            else
                await _client.SendTextMessageAsync(chatId, $"Упс, что-то пошло не так...", cancellationToken: token);
        }

        private async Task HandleEditedMessageAsync(Update update, CancellationToken token)
        {
            await _client.SendTextMessageAsync(update.EditedMessage.Chat.Id, "Опа, да тут сообщения правят -_-", cancellationToken: token);
        }
    }
}
