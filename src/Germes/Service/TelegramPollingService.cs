using System;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Data;
using Germes.Domain.Data;
using Germes.Implementations.Services;
using Germes.Mediators.Extensions;
using Germes.Mediators.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Germes.Service
{
    public class TelegramPollingService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TelegramPollingService> _logger;

        public TelegramPollingService(IServiceProvider serviceProvider, ILogger<TelegramPollingService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();
            var me = await botClient.GetMeAsync(cancellationToken);

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            botClient.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cancellationToken);

            _logger.LogInformation($"Start listening for @{me.Username}");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();
            botClient.StopReceiving();
            
            var me = await botClient.GetMeAsync(cancellationToken);
            _logger.LogInformation($"Stopped listening for @{me.Username}");
        }
        
        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _                                       => exception.ToString()
            };

            _logger.LogError(errorMessage);
            return Task.CompletedTask;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update == null)
                throw new Exception("Update is null");
            if (update.Type == UpdateType.Message)
                await InternalHandleMessageAsync(update, botClient, cancellationToken);
            else if (update.Type == UpdateType.EditedMessage)
                await HandleEditedMessageAsync(update, botClient, cancellationToken);
        }

        private async Task InternalHandleMessageAsync(Update update, ITelegramBotClient client, CancellationToken token)
        {
            var chatId = update.Message.Chat.Id;
            var res = await HandleMessageAsync(update, isFake: false, token);
            if (res.IsSuccess)
                await client.SendTextMessageAsync(chatId, res.Result.Text, cancellationToken: token);
            else
                await client.SendTextMessageAsync(chatId, $"Упс, что-то пошло не так...", cancellationToken: token);
        }

        private async Task<OperationResult<BotResult>> HandleMessageAsync(Update update, bool isFake,
            CancellationToken token)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            
            var mess = new BotMessage
            {
                ChatId = update.Message.Chat.Id.ToString(),
                Text = update.Message.Text,
                SourceId = isFake ? "Simple" : TelegramSourceAdapter.SourceId
            };
            var res = await mediator.SendSafe<RequestNewMessage, BotResult>(new RequestNewMessage {Message = mess},
                token);
            return res;
        }

        private async Task HandleEditedMessageAsync(Update update, ITelegramBotClient client,
            CancellationToken token)
        {
            await client.SendTextMessageAsync(update.EditedMessage.Chat.Id, "Опа, да тут сообщения правят -_-", cancellationToken: token);
        }
    }
}