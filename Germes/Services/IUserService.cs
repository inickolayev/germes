using Germes.Data.Models;
using Germes.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Germes.Services
{
    public interface IUserService
    {
        /// <summary>
        ///     Получить пользоватея
        /// </summary>
        /// <param name="userId">Id чата</param>
        Task<OperationResult<UserModel>> GetUserAsync(string userId, CancellationToken token = default);
        /// <summary>
        ///     Добавить нового пользователя
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        Task<OperationResult<UserModel>> AddUserAsync(UserModel user, CancellationToken token = default);
    }
}
