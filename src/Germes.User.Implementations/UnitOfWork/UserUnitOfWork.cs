using System.Threading;
using System.Threading.Tasks;
using Germes.User.DataAccess;
using Germes.User.Domain.Repositories;
using Germes.User.Domain.UnitOfWork;
using Germes.User.Implementations.Repositories;

namespace Germes.User.Implementations.UnitOfWork
{
    public class UserUnitOfWork : IUserUnitOfWork
    {
        private readonly UserDbContext _dbContext;

        public IUserRegisterRepository Users { get; }
        
        
        public UserUnitOfWork(UserDbContext dbContext)
        {
            _dbContext = dbContext;
            Users = new UserRegisterRepository(dbContext.Users);
        }
        
        public async Task Complete(CancellationToken ct)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}