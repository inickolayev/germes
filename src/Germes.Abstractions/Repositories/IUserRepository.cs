using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;

namespace Germes.Abstractions.Repositories
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
