using Germes.Data;
using Germes.Data.Results;
using Germes.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;

namespace Germes.Services
{
    /// <summary>
    ///     Сервис для работы с плагинами
    /// </summary>
    public interface IPluginService
    {
        /// <summary>
        ///     Набор плагинов по умолчанию
        /// </summary>
        IEnumerable<IBotPlugin> DefaultPlugins { get; }
        /// <summary>
        ///     Получить все плагины
        /// </summary>
        Task<OperationResult<IEnumerable<IBotPlugin>>> GetPluginsAsync(CancellationToken token);
        /// <summary>
        ///     Добавить плагин
        /// </summary>
        /// <param name="plugin">Плагин</param>
        Task<OperationResult> AddPluginAsync(IBotPlugin plugin, CancellationToken token);
    }
}
