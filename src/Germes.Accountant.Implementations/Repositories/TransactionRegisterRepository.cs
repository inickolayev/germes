using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;
using Germes.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Germes.Accountant.Implementations.Repositories
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public class TransactionRegisterRepository : RegisterRepository<Transaction>, ITransactionRegisterRepository
    {
        public TransactionRegisterRepository(DbSet<Transaction> dbSet) : base(dbSet)
        {
        }
    }
}
