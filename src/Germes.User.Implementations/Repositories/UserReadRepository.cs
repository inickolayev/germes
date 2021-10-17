using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.User.Domain.Repositories;
using DomainModels = Germes.User.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Germes.User.Implementations.Repositories
{
    public class UserReadRepository : IUserReadRepository
    {
        private readonly IQueryable<DomainModels.User> _users;
        
        public UserReadRepository(IQueryable<DomainModels.User> users)
        {
            _users = users.AsNoTracking();
        }

        public async Task<DomainModels.User> GetUserAsync(Guid userId, CancellationToken token)
            => await _users
                .FirstOrDefaultAsync(u => u.Id == userId, token);

        public async Task<DomainModels.User> GetUserAsync(string chatId, CancellationToken token)
            => await _users
                .FirstOrDefaultAsync(u => u.ChatId == chatId, token);
    }
}
