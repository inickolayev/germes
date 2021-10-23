using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Germes.Accountant.Implementations.Repositories
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public class TransactionReadRepository : ITransactionReadRepository
    {
        private readonly IQueryable<Transaction> _transactions;
        private readonly IQueryable<Category> _categories;

        public TransactionReadRepository(IQueryable<Transaction> transactions, IQueryable<Category> categories)
        {
            _transactions = transactions;
            _categories = categories;
        }

        public async Task<decimal> GetBalance(Guid userId, CancellationToken token)
        {
            var incomeSum = _transactions
                .Where(tr => tr.UserId == userId)
                .Where(tr => _categories
                    .FirstOrDefault(c => c.Id == tr.CategoryId)
                    .CategoryType == CategoryTypes.Income)
                .Select(inc => inc.Cost)
                .Sum();
            var expenseSum = _transactions
                .Where(tr => tr.UserId == userId)
                .Where(tr => _categories
                    .FirstOrDefault(c => c.Id == tr.CategoryId)
                    .CategoryType == CategoryTypes.Expose)
                .Select(inc => inc.Cost)
                .Sum();
            var result = incomeSum - expenseSum;
            return result;
        }

        public async Task<decimal> GetBalance(Guid userId, Guid? categoryId, DateTime @from, DateTime to, CancellationToken token)
        {
            var incomeSum = await _transactions
                .Where(tr => tr.UserId == userId)
                .Where(tr => tr.CategoryId == categoryId)
                .Where(tr => tr.CreatedAt >= @from && tr.CreatedAt <= to)
                .Select(inc => inc.Cost)
                .SumAsync(token);
            return incomeSum;
        }
    }
}
