using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Plugins;
using Germes.Abstractions.Services;
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
            private readonly IServiceProvider _serviceProvider;
            private readonly IUserService _userService;
            private readonly ISourceAdapterFactory _sourceAdapterFactory;

            public HelpCommandHandler(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
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
                    var name = await sourceAdapter?.GetName(message.ChatId, token);
                    user = await _userService.AddUserAsync(new AddUserRequest {ChatId = chatId, Name = name}, token);
                    result.AppendLine(WelcomeText.WelcomeUser(user.Name));
                }

                var descriptions = await GetPluginDescriptions(token);
                result.AppendLine(WelcomeText.Help(descriptions));

                return PluginResult.Success(result.ToString());
            }

            private async Task<string> GetPluginDescriptions(CancellationToken token)
            {
                var pluginService = _serviceProvider.GetRequiredService<IPluginService>();
                var plugins = await pluginService.GetPluginsAsync(token);
                var descriptions = string.Join("\n\n", 
                    plugins
                        .Where(p => p.GetType() != typeof(HelpPlugin))
                        .Select(p => p.GetHelpDescription()));
                return descriptions;
            }
        }

        public override string GetHelpDescription()
            => throw new NotImplementedException();
    }
}
