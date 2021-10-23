using System;
using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;
using Inbound = Germes.Accountant.Contracts.Inbound;

namespace Germes.Accountant.Domain.Services
{
    public interface IAccountantService
    {
        Task AddTransaction(Inbound.AddTransactionRequest request, CancellationToken cancellationToken);
        
        Task<decimal> GetBalance(Guid userId, CancellationToken cancellationToken);

        Task<decimal> GetBalance(Guid userId, string categoryName, CancellationToken cancellationToken);

        Task<decimal> GetBalance(Guid userId,
            string categoryName,
            DateTime @from,
            DateTime to,
            CancellationToken cancellationToken);
        
        Task<decimal> GetBalance(Guid userId,
            Guid categoryId,
            CancellationToken cancellationToken);
        
        Task<decimal> GetBalance(Guid userId,
            Guid categoryId,
            DateTime @from,
            DateTime to,
            CancellationToken cancellationToken);

        Task<Category> GetExpenseCategory(Guid userId, string categoryName, CancellationToken token);
        Task<Category> GetIncomeCategory(Guid userId, string categoryName, CancellationToken token);
        Task<Category> AddCategory(Inbound.AddExpenseCategoryRequest request, CancellationToken token);
        Task<Category> AddCategory(Inbound.AddIncomeCategoryRequest request, CancellationToken token);
    }
}