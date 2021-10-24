using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Plugins;
using Germes.Abstractions.Services;
using Germes.Domain.Data;

namespace Germes.Implementations.Plugins
{
    public abstract class CommonPluginCommand : IPluginCommand
    {
        private readonly ICommandParser _commandParser;
        private readonly ICommandHandler _commandHandler;

        protected CommonPluginCommand(ICommandParser commandParser, ICommandHandler commandHandler)
        {
            _commandParser = commandParser;
            _commandHandler = commandHandler;
        }
        
        protected CommonPluginCommand(string commandParserTemplate, ICommandHandler commandHandler)
        {
            _commandParser = new CommandParser(commandParserTemplate);
            _commandHandler = commandHandler;
        }

        public bool Check(BotMessage message)
            => _commandParser.Contains(message.Text);

        public Task<PluginResult> Handle(BotMessage message, CancellationToken token)
        {
            var commandItems = _commandParser.Parse(message.Text);
            return _commandHandler.Handle(message, commandItems, token);
        }

        public abstract string GetHelpDescription();
    }
}