using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Domain.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        ///     Получить пользоватея
        /// </summary>
        /// <param name="chatId">Id чата</param>
        Task<OperationResult<UserModel>> GetUserAsync(string chatId, CancellationToken token = default);
        /// <summary>
        ///     Добавить нового пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        Task<OperationResult<UserModel>> AddUserAsync(UserModel user, CancellationToken token = default);
    }
}
