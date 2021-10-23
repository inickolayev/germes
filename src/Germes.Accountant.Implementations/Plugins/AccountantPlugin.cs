using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Services;
using Germes.Accountant.Domain.Services;
using Germes.Domain.Data;
using Germes.Domain.Plugins;
using Germes.Implementations.Plugins;
using Germes.User.Contracts.Inbound;
using Germes.User.Domain.Services;
using Inbound = Germes.Accountant.Contracts.Inbound;

namespace Germes.Accountant.Implementations.Plugins
{
    /// <summary>
    ///     Плагин, помогающий подсчитывать рассходы
    /// </summary>
    public class AccountantPlugin : IBotPlugin
    {
        private readonly IUserService _userService;
        private readonly IAccountantService _accountantService;
        private readonly IEnumerable<ICommandParser> _commandParsers;
        
        public bool IsAllow => true;

        public AccountantPlugin(IUserService userService,
            IAccountantService accountantService)
        {
            _userService = userService;
            _accountantService = accountantService;
            _commandParsers = new[]
            {
                new CommandParser("{cost}{category}{?comment}")
            };
        }

        public async Task<bool> CheckAsync(BotMessage message, CancellationToken token)
            => _commandParsers.Any(parser => parser.Contains(message.Text));

        public async Task<PluginResult> HandleAsync(BotMessage message, CancellationToken token)
        {
            var text = message.Text;
            var chatId = message.ChatId;
            StringBuilder result = new();
            
            var parser = _commandParsers.First(pr => pr.Contains(text));
            var parsingResult = parser.Parse(text);
            var cost = parsingResult.GetDecimal("cost");
            var user = await _userService.GetUserAsync(chatId, token);

            if (user == null)
            {
                var name = "Some user";
                await _userService.AddUserAsync(new AddUserRequest { ChatId = chatId, Name = name }, token);
                result.AppendLine($"Добро пожаловать, {name}!");
            }
            
            var comment = parsingResult.GetString("comment");
            var categoryName = parsingResult.GetString("category");
            var categoryExpense = await _accountantService.GetExpenseCategory(user.Id, categoryName, token);
            var categoryIncome = await _accountantService.GetIncomeCategory(user.Id, categoryName, token);

            var from = GetCurrentMonthDate(10);
            var to = from.AddMonths(1);

            if (categoryExpense == null && categoryIncome == null)
            {
                categoryExpense = await _accountantService.AddCategory(new Inbound.AddExpenseCategoryRequest
                {
                    UserId = user.Id,
                    Name = categoryName
                }, token);
                result.AppendLine($"Добавлена новая категория расходов \"{categoryName}\"");
            }
            
            if (categoryExpense != null)
            {
                var expense = new Inbound.AddTransactionRequest
                {
                    Cost = cost,
                    Comment = comment,
                    CategoryId = categoryExpense.Id,
                    UserId = user.Id
                };
                await _accountantService.AddTransaction(expense, token);
                
                var categoryBalance = await _accountantService.GetBalance(user.Id, categoryExpense.Id, from, to, token);
                var fromStr = from.ToShortDateString();
                var toStr = to.ToShortDateString();
                result.AppendLine($"Траты по категории \"{categoryName}\" ({fromStr} - {toStr}): {categoryBalance:0} руб.\n");
            }
            else if (categoryIncome != null)
            {
                var income = new Inbound.AddTransactionRequest()
                {
                    Cost = cost,
                    Comment = comment,
                    CategoryId = categoryIncome.Id,
                    UserId = user.Id
                };
                await _accountantService.AddTransaction(income, token);
                
                var categoryBalance = await _accountantService.GetBalance(user.Id, categoryIncome.Id, from, to, token);
                var fromStr = from.ToShortDateString();
                var toStr = from.ToShortDateString();
                result.AppendLine($"Получения по категории \"{categoryName}\" ({fromStr} - {toStr}): {categoryBalance} руб.\n");
            }
           
            var balance = await _accountantService.GetBalance(user.Id, token);
            result.Append($"Остаток по счету: {balance:0} руб.");
            
            return PluginResult.Success(result.ToString());
        }

        private DateTime GetCurrentMonthDate(int dayOfMonth)
        {
            var today = DateTime.Now;
            var newDate = new DateTime(today.Year, today.Month, dayOfMonth);
            return newDate;
        }
    }
}
