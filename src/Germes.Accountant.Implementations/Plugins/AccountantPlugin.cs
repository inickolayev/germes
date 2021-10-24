using System;
using System.Linq;
using Germes.Accountant.Implementations.Translations;
using Germes.Implementations.Plugins;

namespace Germes.Accountant.Implementations.Plugins
{
    /// <summary>
    ///     Плагин, помогающий подсчитывать рассходы
    /// </summary>
    public class AccountantPlugin : CommonBotPlugin
    {
        public override bool IsAllow => true;
        
        public AccountantPlugin(IServiceProvider serviceProvider)
            : base(
                new AddTransactionCommand(serviceProvider),
                new GetBalanceCommand(serviceProvider),
                new GetTransactionsCommand(serviceProvider)
            )
        {
        }
        
        public override string GetHelpDescription()
        {
            var title = AccountantText.HelpTitle;
            var commandDescription = string.Join("\n", _commands.Select(command => command.GetHelpDescription()));
            return string.Join("\n", title, commandDescription);
        }

    }
}
