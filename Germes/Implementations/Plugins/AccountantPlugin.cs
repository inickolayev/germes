using Germes.Domain.Data;
using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using Germes.Domain.Data.Results.Errors;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Data;
using Germes.Domain;
using Germes.Domain.Plugins;
using Germes.Implementations.Services;

namespace Germes.Implementations.Plugins
{
    /// <summary>
    ///     Плагин, помогающий подсчитывать рассходы
    /// </summary>
    public class AccountantPlugin : IBotPlugin
    {
        private readonly Session _session;
        private readonly IAccountantService _accountantService;
        private readonly ICategoryRepository _categoryRepository;

        public bool IsAllow { get; set; }

        public AccountantPlugin(ISessionManager sessionManager, IAccountantService accountantService, ICategoryRepository categoryRepository)
        {
            _session = sessionManager.CurrentSession;
            _accountantService = accountantService;
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<bool>> CheckAsync(BotMessage message, CancellationToken token)
        {
            var text = message.Text;
            var args = text.Split(" ");
            var result = args.Count() == 2 && decimal.TryParse(args[1], out var _);
            return new OperationResult<bool>(result);
        }

        public async Task<OperationResult<BotResult>> HandleAsync(BotMessage message, CancellationToken token)
        {
            // TODO: изменить логику работы
            // логика сейчас наиболее простая: парсим строку, берем второе слово и читаем из него расход
            // TODO: Придумать, что делать при двух одинаковых категориях доходов и рассходов
            var text = message.Text;
            var args = text.Split(" ");
            var categoryName = args[0];
            
            var categoryExpenseRes = await _categoryRepository.GetExpenseCategoryAsync(categoryName, token);
            if (!categoryExpenseRes.IsSuccess)
                return categoryExpenseRes.To<BotResult>();
            var categoryExpense = categoryExpenseRes.Result;
            var categoryIncomeRes = await _categoryRepository.GetIncomeCategoryAsync(categoryName, token);
            if (!categoryIncomeRes.IsSuccess)
                return categoryIncomeRes.To<BotResult>();
            var categoryIncome = categoryIncomeRes.Result;
            var cost = Convert.ToDecimal(args[1]);

            if (categoryExpense != null)
            {
                var expense = new ExpenseModel
                {
                    Cost = cost,
                    Date = DateTime.Now,
                    Category = categoryExpenseRes.Result
                };

                var expenseAddRes = await _accountantService.AddAsync(expense, token);
                if (!expenseAddRes.IsSuccess)
                    return expenseAddRes.To<BotResult>();
            }
            else if (categoryIncome != null)
            {
                var income = new IncomeModel
                {
                    Cost = cost,
                    Date = DateTime.Now,
                    Category = categoryIncomeRes.Result
                };

                var incomeAddRes = await _accountantService.AddAsync(income, token);
                if (!incomeAddRes.IsSuccess)
                    return incomeAddRes.To<BotResult>();
            }
            else
                return new OperationResult<BotResult>(CategoryErrors.CategoryNotExist(categoryName));
           
            var balanceRes = await _accountantService.GetBalanceAsync(token);
            if (!balanceRes.IsSuccess)
                return balanceRes.To<BotResult>();

            var result = $"Остаток: {balanceRes.Result} руб.";
            return new OperationResult<BotResult>(new BotResult
            {
                Text = result
            });
        }
    }
}
