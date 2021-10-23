using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Germes.Accountant.Implementations.Repositories
{
    public class CategoryReadRepository : ICategoryReadRepository
    {
        private readonly IQueryable<Category> _categories;
        private readonly IQueryable<Category> _expenseCategories;
        private readonly IQueryable<Category> _incomeCategories;
        
        public CategoryReadRepository(IQueryable<Category> categories)
        {
            _categories = categories.AsNoTracking();
            _expenseCategories = categories.Where(c => c.CategoryType == CategoryTypes.Expose);
            _incomeCategories = categories.Where(c => c.CategoryType == CategoryTypes.Income);
        }

        public async Task<Category> GetCategory(Guid categoryId, CancellationToken token)
            => await _categories.FirstOrDefaultAsync(c => c.Id == categoryId, token);

        public async Task<Category> GetCategory(Guid userId, string categoryName, CancellationToken token)
            => await _categories
                .Where(c => c.UserId == userId)
                .Where(c => c.CategoryType == CategoryTypes.Expose)
                .SingleOrDefaultAsync(c => c.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase), token);

        public async Task<Category> GetExpenseCategory(Guid userId, string categoryName, CancellationToken token)
           => await _expenseCategories
               .Where(c => c.UserId == userId)
               .Where(c => c.CategoryType == CategoryTypes.Expose)
               .SingleOrDefaultAsync(c => c.Name == categoryName, token);

        public async Task<Category> GetIncomeCategory(Guid userId, string categoryName, CancellationToken token)
            => await _expenseCategories
                .Where(c => c.UserId == userId)
                .Where(c => c.CategoryType == CategoryTypes.Expose)
                .SingleOrDefaultAsync(c => c.Name == categoryName, token);
    }
}
