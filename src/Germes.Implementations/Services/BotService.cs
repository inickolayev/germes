using System;
using Germes.Domain.Data;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Services;
using Germes.Data;
using Microsoft.Extensions.Logging;

namespace Germes.Implementations.Services
{
    public class BotService : IBotService
    {
        private readonly IPluginService _pluginService;
        private readonly ILogger<BotService> _logger;

        public BotService(IPluginService pluginService, ILogger<BotService> logger)
        {
            _pluginService = pluginService;
            _logger = logger;
        }

        public async Task<BotResult> HandleNewMessageAsync(BotMessage message, CancellationToken token)
        {
            try
            {
                var plugins = await _pluginService.GetPluginsAsync(token);
                foreach(var plugin in plugins)
                {
                    var check = await plugin.CheckAsync(message, token);
                    if (check)
                    {
                        var result = await plugin.HandleAsync(message, token);
                        if (result.IsSuccess)
                        {
                            return new BotResult(result.Result.Message, result.Result.NeedToAnswer);
                        }
                        else
                        {
                            return new BotResult(result.Error.Message);
                        }
                    }
                }

                return new BotResult("Ни одного активного плагина не найдено!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new BotResult("Ошибка обработки!");
            }
        }
    }
}
