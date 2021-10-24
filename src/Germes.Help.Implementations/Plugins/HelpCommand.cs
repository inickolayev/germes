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
    public class HelpCommand : CommonPluginCommand
    {
        private static readonly string[] CommandTemplates =  new[] { "/help", "помощь" };
        
        public HelpCommand(IServiceProvider serviceProvider)
            : base(new GroupCommandParser(CommandTemplates), new HelpCommandHandler(serviceProvider))
        {
        }

        private class HelpCommandHandler : ICommandHandler
        {
            private readonly IUserService _userService;
            private readonly ISourceAdapterFactory _sourceAdapterFactory;

            public HelpCommandHandler(IServiceProvider serviceProvider)
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
                    result.AppendLine(WelcomeText.WelcomeUser(user.Name));
                }
                
                result.AppendLine(WelcomeText.Help("-"));

                return PluginResult.Success(result.ToString());
            }
        }
    }
}
