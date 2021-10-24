using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Plugins;
using Germes.Domain.Data;
using Germes.Domain.Plugins;

namespace Germes.Implementations.Plugins
{
    public abstract class CommonBotPlugin : IBotPlugin
    {
        private readonly IPluginCommand[] _commands;
        public abstract bool IsAllow { get; }

        protected CommonBotPlugin(params IPluginCommand[] commands)
        {
            _commands = commands;
        }
        
        public Task<bool> CheckAsync(BotMessage message, CancellationToken token)
            => Task.FromResult(_commands.Any(command => command.Check(Normalize(message))));

        public Task<PluginResult> Handle(BotMessage message, CancellationToken token)
            => _commands.First(command => command.Check(Normalize(message))).Handle(Normalize(message), token);

        protected virtual BotMessage Normalize(BotMessage message)
        {
            return new BotMessage
            {
                ChatId = message.ChatId,
                SourceId = message.SourceId,
                Text = message.Text.ToLower()
            };
        }
    }
}