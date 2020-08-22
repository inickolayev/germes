using Germes.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Germes.Services
{
    public class InitService : IHostedService
    {
        private readonly IServiceProvider _service;
        private readonly BotSettings _botSettings;

        public InitService(IServiceProvider service, BotSettings botSettings)
        {
            _service = service;
            _botSettings = botSettings;
        }

        public async Task StartAsync(CancellationToken token)
        {
            using (var scope = _service.CreateScope())
            {
                var client = scope.ServiceProvider.GetService<TelegramBotClient>();
                await client.SetWebhookAsync(_botSettings.NgrokUrl, cancellationToken: token);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
