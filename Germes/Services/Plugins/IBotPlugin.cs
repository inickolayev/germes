using Germes.Data;
using Germes.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Services.Plugins
{
    /// <summary>
    ///     Плагин обработки сообщений
    /// </summary>
    public interface IBotPlugin
    {
        /// <summary>
        ///     Разрешен ли плагин
        /// </summary>
        public bool IsAllow { get; }

        /// <summary>
        ///     Подходит ли сообщение для данного плагина
        /// </summary>
        /// <param name="message">Сообщение</param>
        Task<OperationResult<bool>> CheckAsync(BotMessage message, CancellationToken token = default);
        /// <summary>
        ///     Обработать сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        Task<OperationResult<BotResult>> HandleAsync(Session session, BotMessage message, CancellationToken token = default);
    }
}
