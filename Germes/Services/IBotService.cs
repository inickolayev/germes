using Germes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Services
{
    /// <summary>
    ///     Бот-обработчик
    /// </summary>
    public interface IBotService
    {
        /// <summary>
        ///     Обработать новое сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Результат обработки сообщения</returns>
        Task<BotResult> HandleNewMessageAsync(BotMessage message, CancellationToken token = default);
    }
}
