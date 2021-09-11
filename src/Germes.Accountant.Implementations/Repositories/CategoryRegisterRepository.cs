using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results.Errors;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;

namespace Germes.Accountant.Implementations.Repositories
{
    /// <summary>
    ///     Сервис хранения категорий
    /// </summary>
    public class CategoryRegisterRepository : ICategoryRegisterRepository
    {
        private readonly Session _session;
        private readonly List<ExpenseCategory> _expenseCategories;
        private readonly List<IncomeCategory> _incomeCategories;

        private static readonly Dictionary<Session, List<ExpenseCategory>> _expenseCategoriesDb = new Dictionary<Session, List<ExpenseCategory>>();
        private static readonly Dictionary<Session, List<IncomeCategory>> _incomeCategoriesDb = new Dictionary<Session, List<IncomeCategory>>();

        public List<ExpenseCategory> DefaultExpenseCategories =>
            new List<string>
            {
                "Продукты",
                "Алкоголь",
                "Табак",
                "Животные",
                "Подписки"
            }
            .Select(name => new ExpenseCategory { Name = name })
            .ToList();

        public List<IncomeCategory> DefaultIncomeCategories =>
            new List<string>
            {
                "ЗП",
                "Аванс",
                "Подработка"
            }
            .Select(name => new IncomeCategory { Name = name })
            .ToList();

        public CategoryRegisterRepository()
        {
        }

        public async Task<ExpenseCategory> GetExpenseCategoryAsync(string category, CancellationToken token)
           => _expenseCategories.SingleOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase));

        public async Task<ExpenseCategory> AddExpenseCategoryAsync(string category, string description, CancellationToken token)
        {
            if (_expenseCategories.Any(c => c.Name == category))
                throw new Exception(CategoryErrors.CategoryNotExist(category).Message);
            var ctg = new ExpenseCategory
            {
                Name = category,
                Description = description
            };
            _expenseCategories.Add(ctg);
            return ctg;
        }

        public async Task<IncomeCategory> GetIncomeCategoryAsync(string category, CancellationToken token)
           => _incomeCategories.SingleOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase));

        public async Task<IncomeCategory> AddIncomeCategoryAsync(string category, string description, CancellationToken token)
        {
            if (_incomeCategories.Any(c => c.Name == category))
                throw new Exception(CategoryErrors.CategoryNotExist(category).Message);
            var ctg = new IncomeCategory
            {
                Name = category,
                Description = description
            };
            _incomeCategories.Add(ctg);
            return ctg;
        }
    }
}
