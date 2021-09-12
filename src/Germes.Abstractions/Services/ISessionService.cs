using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Accountant.Domain.Models;

namespace Germes.Abstractions.Services
{
    public interface ISessionService
    {
        /// <summary>
        ///     Получить сессию по чату
        /// </summary>
        /// <param name="chatId">Id чата</param>
        Task<OperationResult<Session>> GetSessionAsync(string chatId, CancellationToken token);
        /// <summary>
        ///     Добавить новую сессию
        /// </summary>
        /// <param name="chatId">Id чата</param>
        Task<OperationResult<Session>> AddSessionAsync(string chatId, CancellationToken token);
    }
}
