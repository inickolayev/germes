using System;

namespace Germes.Domain.Exceptions
{
    public class CommandParserCommandNotValidException : Exception
    {
        public string Command { get; }
        public string Pattern { get; }

        public CommandParserCommandNotValidException(string command, string pattern)
            : base($"Not valid \"{command}\" for pattern \"{pattern}\"")
        {
            Command = command;
            Pattern = pattern;
        }
    }
}