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
    public class GetBalanceCommand : CommonPluginCommand
    {
        private const string CommandTemplate = "баланс";
        
        public GetBalanceCommand(IServiceProvider serviceProvider)
            : base(CommandTemplate, new GetBalanceCommandHandler(serviceProvider))
        {
        }

        class GetBalanceCommandHandler : ICommandHandler
        {
            private readonly IUserService _userService;
            private readonly ISourceAdapterFactory _sourceAdapterFactory;
            private readonly IAccountantService _accountantService;

            public GetBalanceCommandHandler(IServiceProvider serviceProvider)
            {
                _userService = serviceProvider.GetRequiredService<IUserService>();
                _sourceAdapterFactory = serviceProvider.GetRequiredService<ISourceAdapterFactory>();
                _accountantService = serviceProvider.GetRequiredService<IAccountantService>();
            }

            public async Task<PluginResult> Handle(BotMessage message, CommandItems commandItems,
                CancellationToken token)
            {
                var chatId = message.ChatId;
                StringBuilder result = new();

                var user = await _userService.GetUserAsync(chatId, token);

                if (user == null)
                {
                    var sourceAdapter = _sourceAdapterFactory.GetAdapter(message.SourceId);
                    var name = await sourceAdapter.GetName(message.ChatId, token);
                    user = await _userService.AddUserAsync(new AddUserRequest {ChatId = chatId, Name = name}, token);
                    result.AppendLine(AccountantText.WelcomeUser(name));
                    result.AppendLine();
                }

                var balance = await _accountantService.GetBalance(user.Id, token);
                result.AppendLine(AccountantText.RemainingBalance(balance));

                return PluginResult.Success(result.ToString());
            }
        }
    }
}
