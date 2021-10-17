using System;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.User.Domain.Repositories
{
    public interface IUserReadRepository
    {
        Task<Models.User> GetUserAsync(Guid userId, CancellationToken token);
        Task<Models.User> GetUserAsync(string chatId, CancellationToken token);
    }
}