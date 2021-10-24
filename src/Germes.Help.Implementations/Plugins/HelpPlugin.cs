using System;
using Germes.Implementations.Plugins;

namespace Germes.Help.Implementations.Plugins
{
    public class HelpPlugin : CommonBotPlugin
    {
        public override bool IsAllow => true;

        public HelpPlugin(IServiceProvider serviceProvider)
            : base(
                new WelcomeCommand(serviceProvider),
                new HelpCommand(serviceProvider)
            )
        {
        }
    }
}
