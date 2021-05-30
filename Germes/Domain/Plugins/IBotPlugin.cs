using Germes.Domain.Data;
using Germes.Domain.Data.Results;
using System.Threading;
using System.Threading.Tasks;
using Germes.Data;

namespace Germes.Domain.Plugins
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
        Task<OperationResult<BotResult>> HandleAsync(BotMessage message, CancellationToken token = default);
    }
}
