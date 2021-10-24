using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Plugins;
using Germes.Abstractions.Services;
using Germes.Accountant.Domain.Services;
using Germes.Domain.Data;
using Germes.Help.Implementations.Translations;
using Germes.Implementations.Plugins;
using Germes.User.Contracts.Inbound;
using Germes.User.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Germes.Help.Implementations.Plugins
{
    public class WelcomeCommand : CommonPluginCommand
    {
        private const string CommandTemplate = "/start";
        
        public WelcomeCommand(IServiceProvider serviceProvider)
            : base(CommandTemplate, new WelcomeCommandHandler(serviceProvider))
        {
        }

        private class WelcomeCommandHandler : ICommandHandler
        {
            private readonly IUserService _userService;
            private readonly ISourceAdapterFactory _sourceAdapterFactory;

            public WelcomeCommandHandler(IServiceProvider serviceProvider)
            {
                _userService = serviceProvider.GetRequiredService<IUserService>();
                _sourceAdapterFactory = serviceProvider.GetRequiredService<ISourceAdapterFactory>();
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
                }
                result.AppendLine(WelcomeText.WelcomeUser(user.Name));

                return PluginResult.Success(result.ToString());
            }
        }
    }
}
