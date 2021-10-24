using System.Collections.Generic;
using System.Linq;
using Germes.Abstractions.Models;
using Germes.Abstractions.Services;

namespace Germes.Implementations.Plugins
{
    public class GroupCommandParser : ICommandParser
    {
        private readonly List<ICommandParser> _commandParsers = new();
        
        public GroupCommandParser(string[] patterns)
        {
            foreach (var pattern in patterns)
            {
                _commandParsers.Add(new CommandParser(pattern));
            }
        }
        
        public bool Contains(string command)
            => _commandParsers.Any(parser => parser.Contains(command));

        public CommandItems Parse(string command)
        {
            var currentParser = _commandParsers.First(parser => parser.Contains(command));
            return currentParser.Parse(command);
        }
    }
}