using System;
using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;
using Inbound = Germes.Accountant.Contracts.Inbound;
using Dto = Germes.Accountant.Contracts.Dto;

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

        Task<Dto.ExpenseCategoryDto> GetExpenseCategory(Guid userId, string categoryName, CancellationToken token);
        Task<Dto.IncomeCategoryDto> GetIncomeCategory(Guid userId, string categoryName, CancellationToken token);
        Task<Dto.ExpenseCategoryDto> AddCategory(Inbound.AddExpenseCategoryRequest request, CancellationToken token);
        Task<Dto.IncomeCategoryDto> AddCategory(Inbound.AddIncomeCategoryRequest request, CancellationToken token);
    }
}