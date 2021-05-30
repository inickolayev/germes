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
        private readonly List<ExpenseCategoryModel> _expenseCategories;
        private readonly List<IncomeCategoryModel> _incomeCategories;

        private static readonly Dictionary<Session, List<ExpenseCategoryModel>> _expenseCategoriesDb = new Dictionary<Session, List<ExpenseCategoryModel>>();
        private static readonly Dictionary<Session, List<IncomeCategoryModel>> _incomeCategoriesDb = new Dictionary<Session, List<IncomeCategoryModel>>();

        public List<ExpenseCategoryModel> DefaultExpenseCategories =>
            new List<string>
            {
                "Продукты",
                "Алкоголь",
                "Табак",
                "Животные",
                "Подписки"
            }
            .Select(name => new ExpenseCategoryModel { Name = name })
            .ToList();

        public List<IncomeCategoryModel> DefaultIncomeCategories =>
            new List<string>
            {
                "ЗП",
                "Аванс",
                "Подработка"
            }
            .Select(name => new IncomeCategoryModel { Name = name })
            .ToList();

        public CategoryRepository(ISessionManager sessionManager)
        {
            _session = sessionManager.CurrentSession;
            if (!_expenseCategoriesDb.ContainsKey(_session))
                _expenseCategoriesDb.Add(_session, new List<ExpenseCategoryModel>());
            _expenseCategories = _expenseCategoriesDb[_session];
            if (!_incomeCategoriesDb.ContainsKey(_session))
                _incomeCategoriesDb.Add(_session, new List<IncomeCategoryModel>());
            _incomeCategories = _incomeCategoriesDb[_session];

            // TOOD: Заменить на более удобный способ
            if (!_expenseCategories.Any())
                _expenseCategories.AddRange(DefaultExpenseCategories);
            if (!_incomeCategories.Any())
                _incomeCategories.AddRange(DefaultIncomeCategories);
        }

        public async Task<OperationResult<ExpenseCategoryModel>> GetExpenseCategoryAsync(string category, CancellationToken token)
           => new OperationResult<ExpenseCategoryModel>(_expenseCategories.SingleOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase)));

        public async Task<OperationResult<ExpenseCategoryModel>> AddExpenseCategoryAsync(string category, string description, CancellationToken token)
        {
            if (_expenseCategories.Any(c => c.Name == category))
                return new OperationResult<ExpenseCategoryModel>(CategoryErrors.CategoryNotExist(category));
            var ctg = new ExpenseCategoryModel
            {
                Name = category,
                Description = description
            };
            _expenseCategories.Add(ctg);
            return new OperationResult<ExpenseCategoryModel>(ctg);
        }

        public async Task<OperationResult<IncomeCategoryModel>> GetIncomeCategoryAsync(string category, CancellationToken token)
           => new OperationResult<IncomeCategoryModel>(_incomeCategories.SingleOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase)));

        public async Task<OperationResult<IncomeCategoryModel>> AddIncomeCategoryAsync(string category, string description, CancellationToken token)
        {
            if (_incomeCategories.Any(c => c.Name == category))
                return new OperationResult<IncomeCategoryModel>(CategoryErrors.CategoryNotExist(category));
            var ctg = new IncomeCategoryModel
            {
                Name = category,
                Description = description
            };
            _incomeCategories.Add(ctg);
            return new OperationResult<IncomeCategoryModel>(ctg);
        }
    }
}
