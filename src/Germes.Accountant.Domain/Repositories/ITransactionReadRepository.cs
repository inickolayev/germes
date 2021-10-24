using System;
using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;

namespace Germes.Accountant.Domain.Repositories
{
    public interface ITransactionReadRepository
    {
        Task<decimal> GetBalance(Guid userId, CancellationToken token);
        Task<decimal> GetBalance(Guid userId, Guid? categoryId, DateTime from, DateTime to, CancellationToken token);
        Task<Transaction[]> GetTransactions(Guid userId, Guid? categoryId, DateTime @from, DateTime to, CancellationToken cancellationToken);
    }
}
