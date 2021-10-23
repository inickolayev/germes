using Germes.Implementations.Repositories;
using Germes.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using DomainModels = Germes.User.Domain.Models;

namespace Germes.User.Implementations.Repositories
{
    public class UserRegisterRepository : RegisterRepository<DomainModels.User>, IUserRegisterRepository
    {
        public UserRegisterRepository(DbSet<DomainModels.User> dbSet) : base(dbSet)
        {
        }
    }
}
