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
        private readonly IAccountantRepository _accountantRepository;
        private readonly ICategoryRepository _categoryRepository;

        public bool IsAllow { get; set; }

        private const string CommonPattern = "{d} ?{category} ?{subCategory}";
        private const string SubPattern = "{d} {subCategory}";

        public AccountantPlugin(ISessionManager sessionManager, IAccountantRepository accountantRepository, ICategoryRepository categoryRepository)
        {
            _session = sessionManager.CurrentSession;
            _accountantRepository = accountantRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<OperationResult<bool>> CheckAsync(BotMessage message, CancellationToken token)
        {
            var text = message.Text;
            var args = text.Split(" ");
            var result = args.Count() <= 3 && decimal.TryParse(args[0], out var _);
            return new OperationResult<bool>(result);
        }

        public async Task<OperationResult<BotResult>> HandleAsync(BotMessage message, CancellationToken token)
        {
            // TODO: изменить логику работы
            // логика сейчас наиболее простая: парсим строку, берем второе слово и читаем из него расход
            // TODO: Придумать, что делать при двух одинаковых категориях доходов и рассходов
            var text = message.Text;
            var args = text.Split(" ");
            var cost = Convert.ToDecimal(args[0]);
            
            var categoryName = args[1];
            var categoryExpense = await _categoryRepository.GetExpenseCategoryAsync(categoryName, token);
            var categoryIncome = await _categoryRepository.GetIncomeCategoryAsync(categoryName, token);

            if (categoryExpense != null)
            {
                var expense = new Expense
                {
                    Cost = cost,
                    Date = DateTime.Now,
                    Category = categoryExpense
                };

                await _accountantRepository.AddAsync(expense, token);
            }
            else if (categoryIncome != null)
            {
                var income = new Income
                {
                    Cost = cost,
                    Date = DateTime.Now,
                    Category = categoryIncome
                };

                await _accountantRepository.AddAsync(income, token);
            }
            else
            {
                return new OperationResult<BotResult>(CategoryErrors.CategoryNotExist(categoryName));
            }
           
            var balance = await _accountantRepository.GetBalanceAsync(token);

            var result = $"Остаток: {balance} руб.";
            return new OperationResult<BotResult>(new BotResult
            {
                Text = result
            });
        }
    }
}
