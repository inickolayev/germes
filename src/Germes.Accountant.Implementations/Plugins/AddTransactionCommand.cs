using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Plugins;
using Germes.Abstractions.Services;
using Germes.Accountant.Domain.Services;
using Germes.Accountant.Implementations.Translations;
using Germes.Domain.Data;
using Germes.Implementations.Plugins;
using Germes.User.Contracts.Inbound;
using Germes.User.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Inbound = Germes.Accountant.Contracts.Inbound;

namespace Germes.Accountant.Implementations.Plugins
{
    public class AddTransactionCommand : CommonPluginCommand
    {
        private const string CommandTemplate = "{cost}{category}{?comment}";
        
        public AddTransactionCommand(IServiceProvider serviceProvider)
            : base(CommandTemplate, new AddTransactionCommandHandler(serviceProvider))
        {
        }

        public override string GetHelpDescription()
            => $"* \"{CommandTemplate}\" - {AccountantText.AddTransactionDescription}";

        class AddTransactionCommandHandler : ICommandHandler
        {
            private readonly IUserService _userService;
            private readonly ISourceAdapterFactory _sourceAdapterFactory;
            private readonly IAccountantService _accountantService;

            public AddTransactionCommandHandler(IServiceProvider serviceProvider)
            {
                _userService = serviceProvider.GetRequiredService<IUserService>();
                _sourceAdapterFactory = serviceProvider.GetRequiredService<ISourceAdapterFactory>();
                _accountantService = serviceProvider.GetRequiredService<IAccountantService>();
            }

            public async Task<PluginResult> Handle(BotMessage message, CommandItems commandItems,
                CancellationToken token)
            {
                var text = message.Text;
                var chatId = message.ChatId;
                StringBuilder result = new();

                var cost = commandItems.GetDecimal("cost");
                var user = await _userService.GetUserAsync(chatId, token);

                if (user == null)
                {
                    var sourceAdapter = _sourceAdapterFactory.GetAdapter(message.SourceId);
                    var name = await sourceAdapter.GetName(message.ChatId, token);
                    user = await _userService.AddUserAsync(new AddUserRequest {ChatId = chatId, Name = name}, token);
                    result.AppendLine(AccountantText.WelcomeUser(name));
                }

                var comment = commandItems.GetString("comment");
                var categoryName = commandItems.GetString("category");
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
                    result.AppendLine(AccountantText.AddedNewCategory(categoryName));
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

                    var categoryBalance =
                        await _accountantService.GetBalance(user.Id, categoryExpense.Id, from, to, token);
                    result.AppendLine(
                        AccountantText.AddedNewExpenseTransaction(categoryName, from, to, categoryBalance));
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

                    var categoryBalance =
                        await _accountantService.GetBalance(user.Id, categoryIncome.Id, from, to, token);
                    result.AppendLine(
                        AccountantText.AddedNewIncomeTransaction(categoryName, from, to, categoryBalance));
                }

                var balance = await _accountantService.GetBalance(user.Id, token);
                result.AppendLine();
                result.AppendLine(AccountantText.RemainingBalance(balance));

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
}
