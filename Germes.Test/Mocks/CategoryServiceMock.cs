using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Results;
using Germes.Data.Results.Errors;
using Germes.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Test.Mocks
{
    /// <summary>
    ///     Сервис хранения категорий
    /// </summary>
    public class CategoryServiceMock : ICategoryService, IDisposable
    {
        private readonly Session _session;
        private readonly List<ExpenseCategoryModel> _expenseCategories;
        private readonly List<IncomeCategoryModel> _incomeCategories;

        public static readonly Dictionary<Session, List<ExpenseCategoryModel>> ExpenseCategoriesDb = new Dictionary<Session, List<ExpenseCategoryModel>>();
        public static readonly Dictionary<Session, List<IncomeCategoryModel>> IncomeCategoriesDb = new Dictionary<Session, List<IncomeCategoryModel>>();

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

        public CategoryServiceMock(ISessionManager sessionManager)
        {
            _session = sessionManager.CurrentSession;
            if (!ExpenseCategoriesDb.ContainsKey(_session))
                ExpenseCategoriesDb.Add(_session, new List<ExpenseCategoryModel>());
            _expenseCategories = ExpenseCategoriesDb[_session];
            if (!IncomeCategoriesDb.ContainsKey(_session))
                IncomeCategoriesDb.Add(_session, new List<IncomeCategoryModel>());
            _incomeCategories = IncomeCategoriesDb[_session];

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

        public void Dispose()
        {
            ExpenseCategoriesDb.Clear();
            IncomeCategoriesDb.Clear();
        }
    }
}
