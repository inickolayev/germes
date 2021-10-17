using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.DataAccess;
using Germes.Accountant.Domain.Repositories;
using Germes.Accountant.Domain.UnitOfWork;
using Germes.Accountant.Implementations.Repositories;

namespace Germes.Accountant.Implementations.UnitOfWork
{
    public class AccountantUnitOfWork : IAccountantUnitOfWork
    {
        private readonly AccountantDbContext _dbContext;

        public ICategoryRegisterRepository Categories { get; }
        public ITransactionRegisterRepository Transactions { get; }
        
        
        public AccountantUnitOfWork(AccountantDbContext dbContext)
        {
            _dbContext = dbContext;
            Categories = new CategoryRegisterRepository(dbContext.Categories);
            Transactions = new TransactionRegisterRepository(dbContext.Transactions);
        }
        
        public async Task Complete(CancellationToken ct)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}