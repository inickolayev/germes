using Germes.Accountant.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results.Errors;
using Germes.Abstractions.Repositories;
using Germes.Domain.Exceptions;

namespace Germes.Implementations.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();

        public async Task<User> GetUserAsync(string chatId, CancellationToken token)
            => _users.SingleOrDefault(us => us.ChatId == chatId);

        public async Task<User> AddUserAsync(User user, CancellationToken token)
        {
            if (_users.Any(us => us.ChatId == user.ChatId))
                throw new BusinessException(UserErrors.UserNotExist(user.ChatId));
            _users.Add(user);
            return user;
        }
    }
}
