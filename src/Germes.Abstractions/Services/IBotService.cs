using Germes.Domain.Data;
using System.Threading;
using System.Threading.Tasks;
using Germes.Data;

namespace Germes.Implementations.Services
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
