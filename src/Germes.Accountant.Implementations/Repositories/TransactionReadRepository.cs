﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Extensions;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;

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
            var incomeSum = _transactions
                .Where(tr => tr.UserId == userId)
                .Where(tr => tr.CategoryId == categoryId)
                .Where(tr => tr.CreatedAt.IsBetween(@from, to))
                .Select(inc => inc.Cost)
                .Sum();
            return incomeSum;
        }
    }
}
