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
        Task<User> GetUserAsync(string chatId, CancellationToken token = default);
        /// <summary>
        ///     Добавить нового пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        Task<User> AddUserAsync(User user, CancellationToken token = default);
    }
}
