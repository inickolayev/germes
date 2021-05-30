using Germes.Domain.Data;
using Germes.Domain.Data.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Implementations.Services
{
    public interface ISessionService
    {
        /// <summary>
        ///     Получить сессию по чату
        /// </summary>
        /// <param name="chatId">Id чата</param>
        Task<OperationResult<Session>> GetSessionAsync(string chatId, CancellationToken token = default);
        /// <summary>
        ///     Добавить новую сессию
        /// </summary>
        /// <param name="chatId">Id чата</param>
        Task<OperationResult<Session>> AddSessionAsync(string chatId, CancellationToken token = default);
    }
}
