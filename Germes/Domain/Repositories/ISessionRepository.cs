using Germes.Domain.Data;
using Germes.Domain.Data.Results;
using System.Threading;
using System.Threading.Tasks;
using Germes.Domain.Data.Models;

namespace Germes.Domain.Repositories
{
    public interface ISessionRepository
    {
        /// <summary>
        ///     Получить сессию по чату
        /// </summary>
        /// <param name="chatId">Id чата</param>
        Task<OperationResult<SessionModel>> GetSessionAsync(string chatId, CancellationToken token = default);
        /// <summary>
        ///     Добавить новую сессию
        /// </summary>
        /// <param name="chatId">Id чата</param>
        Task<OperationResult<SessionModel>> AddSessionAsync(string chatId, CancellationToken token = default);
    }
}
