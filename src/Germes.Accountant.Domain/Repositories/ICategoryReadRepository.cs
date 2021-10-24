using System;
using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;

namespace Germes.Accountant.Domain.Repositories
{
    public interface ICategoryReadRepository
    {
        Task<Category[]> GetCategories(Guid userId, CancellationToken cancellationToken);
        Task<Category> GetExpenseCategory(Guid userId, string categoryName, CancellationToken token);
        
        Task<Category> GetIncomeCategory(Guid userId, string categoryName, CancellationToken token);
        
        Task<Category> GetCategory(Guid categoryId, CancellationToken token);
        Task<Category> GetCategory(Guid userId, string categoryName, CancellationToken token);
    }
}
