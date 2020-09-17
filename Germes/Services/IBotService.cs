using Germes.Data;
using Germes.Data.Results;
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
        Task<OperationResult<BotResult>> HandleNewMessageAsync(BotMessage message, CancellationToken token = default);
    }
}
