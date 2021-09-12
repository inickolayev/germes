using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Services;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;
using Germes.Accountant.Domain.Services;
using Germes.Domain.Data;
using Germes.Domain.Plugins;
using Germes.Implementations.Plugins;

namespace Germes.Accountant.Implementations.Plugins
{
    /// <summary>
    ///     Плагин, помогающий подсчитывать рассходы
    /// </summary>
    public class AccountantPlugin : IBotPlugin
    {
        private readonly IAccountantReadRepository _accountantRepository;
        private readonly IAccountantRegisterRepository _accountantRegisterRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryRegisterRepository _categoryRegisterRepository;
        private readonly IEnumerable<ICommandParser> _commandParsers;
        
        public bool IsAllow => true;

        private const string CommonPattern = "{cost}{comment}{?category}";

        public AccountantPlugin(IAccountantReadRepository accountantRepository,
            IAccountantRegisterRepository accountantRegisterRepository,
            ICategoryReadRepository categoryReadRepository,
            ICategoryRegisterRepository categoryRegisterRepository,
            IAccountantService accountantService)
        {
            _accountantRepository = accountantRepository;
            _accountantRegisterRepository = accountantRegisterRepository;
            _categoryReadRepository = categoryReadRepository;
            _categoryRegisterRepository = categoryRegisterRepository;
            _commandParsers = new[]
            {
                new CommandParser("{cost}{?comment}{?category}")
            };
        }

        public async Task<bool> CheckAsync(BotMessage message, CancellationToken token)
            => _commandParsers.Any(parser => parser.Contains(message.Text));

        public async Task<PluginResult> HandleAsync(BotMessage message, CancellationToken token)
        {
            var text = message.Text;
            var parser = _commandParsers.First(pr => pr.Contains(text));
            var parsingResult = parser.Parse(text);
            var cost = parsingResult.GetDecimal("cost");

            var comment = parsingResult.GetString("comment");
            var categoryName = parsingResult.GetString("category");
            var categoryExpense = await _categoryReadRepository.GetExpenseCategoryAsync(categoryName, token);
            var categoryIncome = await _categoryReadRepository.GetIncomeCategoryAsync(categoryName, token);

            if (categoryExpense != null)
            {
                var expense = new Expense
                {
                    Cost = cost,
                    Date = DateTime.Now,
                    Category = categoryExpense
                };

                await _accountantRegisterRepository.AddAsync(expense, token);
            }
            else if (categoryIncome != null)
            {
                var income = new Income
                {
                    Cost = cost,
                    Date = DateTime.Now,
                    Category = categoryIncome
                };

                await _accountantRegisterRepository.AddAsync(income, token);
            }
            else
            {
                await _categoryRegisterRepository.AddExpenseCategoryAsync(categoryName, "", token);
            }
           
            var categoryBalance = await _accountantRepository.GetBalanceAsync(string.Empty, token);
            var balance = await _accountantRepository.GetBalanceAsync(string.Empty, token);

            var result = $"Остаток по категории: {balance} руб.\n"
                + $"Остаток по счету: {balance} руб.";
            return PluginResult.Success(result);
        }
    }
}
