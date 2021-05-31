using Germes.Domain.Data;
using Germes.Domain.Data.Results;
using System.Threading;
using System.Threading.Tasks;
using Germes.Data;
using Germes.Domain;

namespace Germes.Implementations.Services
{
    public class BotService : IBotService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IPluginService _pluginService;

        public BotService(ISessionRepository sessionRepository, IPluginService pluginService)
        {
            _sessionRepository = sessionRepository;
            _pluginService = pluginService;
        }

        public async Task<OperationResult<BotResult>> HandleNewMessageAsync(BotMessage message, CancellationToken token)
        {
            var pluginsRes = await _pluginService.GetPluginsAsync(token);
            if (!pluginsRes.IsSuccess)
                return pluginsRes.To<BotResult>();
            var plugins = pluginsRes.Result;
            foreach(var plugin in plugins)
            {
                var checkRes = await plugin.CheckAsync(message, token);
                if (!checkRes.IsSuccess)
                    return checkRes.To<BotResult>();
                if (checkRes.Result)
                {
                    var result = await plugin.HandleAsync(message, token);
                    if (result.IsSuccess)
                        return result;
                    // TODO: обдумать лучшую вариацию обработки ошибок
                    else if (result.Error is BusinessError berror)
                        return new OperationResult<BotResult>(new BotResult
                        {
                            Text = berror.Message,
                        });
                    else
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
