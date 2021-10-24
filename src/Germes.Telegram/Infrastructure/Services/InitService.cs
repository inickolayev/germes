using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ngrok.Adapter.Service;
using Telegram.Bot;

namespace Germes.Telegram.Infrastructure.Services
{
    public class InitService : IHostedService
    {
        private readonly IServiceProvider _service;
        private readonly INgrokService _ngrokService;
        private readonly ILogger<InitService> _logger;

        public InitService(IServiceProvider service, INgrokService ngrokService, ILogger<InitService> logger)
        {
            _service = service;
            _ngrokService = ngrokService;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var tunnelUrl = await _ngrokService.GetHttpsTunnelUrl();
            var ngrokUrl = $"{tunnelUrl}/api/bot";
            using var scope = _service.CreateScope();
            var client = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();
            await client.SetWebhookAsync(ngrokUrl, cancellationToken: cancellationToken);
            
            var me = await client.GetMeAsync(cancellationToken);
            _logger.LogInformation($"Start listening for @{me.Username}");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _service.CreateScope();
            var client = scope.ServiceProvider.GetRequiredService<TelegramBotClient>();
            await client.DeleteWebhookAsync(cancellationToken);
            
            var me = await client.GetMeAsync(cancellationToken);
            _logger.LogInformation($"Stopped listening for @{me.Username}");
        }
    }
}
