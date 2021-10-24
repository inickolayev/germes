using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Plugins;
using Germes.Abstractions.Services;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Services;
using Germes.Accountant.Implementations.Translations;
using Germes.Domain.Data;
using Germes.Implementations.Plugins;
using Germes.User.Contracts.Inbound;
using Germes.User.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Germes.Accountant.Implementations.Plugins
{
    public class GetTransactionsCommand : CommonPluginCommand
    {
        private const string CommandTemplate = "список";
        
        public GetTransactionsCommand(IServiceProvider serviceProvider)
            : base(CommandTemplate, new GetTransactionsCommandHandler(serviceProvider))
        {
        }

        public override string GetHelpDescription()
            => $"* \"{CommandTemplate}\" - {AccountantText.GetBalanceDescription}";

        class GetTransactionsCommandHandler : ICommandHandler
        {
            private readonly IUserService _userService;
            private readonly ISourceAdapterFactory _sourceAdapterFactory;
            private readonly IAccountantService _accountantService;

            public GetTransactionsCommandHandler(IServiceProvider serviceProvider)
            {
                _userService = serviceProvider.GetRequiredService<IUserService>();
                _sourceAdapterFactory = serviceProvider.GetRequiredService<ISourceAdapterFactory>();
                _accountantService = serviceProvider.GetRequiredService<IAccountantService>();
            }

            public async Task<PluginResult> Handle(BotMessage message, CommandItems commandItems,
                CancellationToken cancellationToken)
            {
                var chatId = message.ChatId;
                StringBuilder result = new();

                var user = await _userService.GetUserAsync(chatId, cancellationToken);

                if (user == null)
                {
                    var sourceAdapter = _sourceAdapterFactory.GetAdapter(message.SourceId);
                    var name = await sourceAdapter.GetName(message.ChatId, cancellationToken);
                    user = await _userService.AddUserAsync(new AddUserRequest {ChatId = chatId, Name = name}, cancellationToken);
                    result.AppendLine(AccountantText.WelcomeUser(name));
                    result.AppendLine();
                }

                var from = GetCurrentMonthDate(10);
                var to = from.AddMonths(1);
                var transactions = await _accountantService
                    .GetTransactions(user.Id, null as Guid?, from, to, cancellationToken);
                
                if (!transactions.Any())
                {
                    result.AppendLine(AccountantText.NoTransactionsFound);
                }
                else
                {
                    var categories = (await _accountantService.GetCategories(user.Id, cancellationToken))
                        .ToDictionary(c => c.Id, c => c);
                    
                    var groupedTransactions = transactions
                        .GroupBy(tr => GetCurrentDateTime(tr.CreatedAt).Date);
                    foreach (var group in groupedTransactions)
                    {
                        if (group.Count() > 1)
                        {
                            result.AppendLine($"{group.Key:dd.MM}");
                            foreach (var transaction in group)
                            {
                                GetShortTransaction(transaction, result, categories);
                            }
                        }
                        else
                        {
                            result.Append($"{group.Key:dd.MM} ");
                            GetShortTransaction(group.First(), result, categories);
                        }
                    }
                }
                
                return PluginResult.Success(result.ToString());
            }

            private static void GetShortTransaction(Transaction transaction, StringBuilder result, Dictionary<Guid, Category> categories)
            {
                var category = categories[transaction.CategoryId];
                var createdAt = GetCurrentDateTime(transaction.CreatedAt);
                
                result.Append($"{createdAt:HH:mm}:");
                result.Append($" {(category.CategoryType == CategoryTypes.Expose ? "-" : "+")}{transaction.Cost:0} руб");
                result.Append($" {category.Name}");
                if (!string.IsNullOrWhiteSpace(transaction.Comment))
                {
                    result.Append($" {transaction.Comment}");
                }

                result.AppendLine();
            }
            
            private static DateTime GetCurrentDateTime(DateTime dateTime)
                => TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);

            private static DateTime GetCurrentMonthDate(int dayOfMonth)
            {
                var today = DateTime.Now;
                var newDate = new DateTime(today.Year, today.Month, dayOfMonth);
                return newDate;
            }
        }
    }
}
