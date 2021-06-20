using Germes.Domain.Data;
using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using Germes.Domain.Data.Results.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Implementations.Services;
using ICategoryRepository = Germes.Domain.ICategoryRepository;

namespace Germes.Implementations.Repositories
{
    /// <summary>
    ///     Сервис хранения категорий
    /// </summary>
    public class CategoryRepository : ICategoryRepository
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

        public CategoryRepository(ISessionManager sessionManager)
        {
            _session = sessionManager.CurrentSession;
            if (!_expenseCategoriesDb.ContainsKey(_session))
                _expenseCategoriesDb.Add(_session, new List<ExpenseCategory>());
            _expenseCategories = _expenseCategoriesDb[_session];
            if (!_incomeCategoriesDb.ContainsKey(_session))
                _incomeCategoriesDb.Add(_session, new List<IncomeCategory>());
            _incomeCategories = _incomeCategoriesDb[_session];

            // TOOD: Заменить на более удобный способ
            if (!_expenseCategories.Any())
                _expenseCategories.AddRange(DefaultExpenseCategories);
            if (!_incomeCategories.Any())
                _incomeCategories.AddRange(DefaultIncomeCategories);
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
