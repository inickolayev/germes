using System;
using System.Threading;
using System.Threading.Tasks;
using Inbound = Germes.User.Contracts.Inbound;

namespace Germes.User.Domain.Services
{
    public interface IUserService
    {
        Task<Models.User> GetUserAsync(Guid userId, CancellationToken token);
        Task<Models.User> GetUserAsync(string chatId, CancellationToken token);
        Task<Models.User> AddUserAsync(Inbound.AddUserRequest user, CancellationToken token);
    }
}