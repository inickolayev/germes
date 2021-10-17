using System;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Accountant.Domain.Repositories
{
    public interface ITransactionReadRepository
    {
        Task<decimal> GetBalance(Guid userId, CancellationToken token);
        Task<decimal> GetBalance(Guid userId, Guid? categoryId, DateTime from, DateTime to, CancellationToken token);
    }
}
