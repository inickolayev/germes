using System;

namespace Germes.Domain.Exceptions
{
    public class CommandParserItemNotExistsException : Exception
    {
        public string Key { get; }
        public string Command { get; }

        public CommandParserItemNotExistsException(string key, string command)
            : base($"Key \"{key}\" does not exists in command \"{command}\"")
        {
            Command = command;
            Key = key;
        }
        
        public CommandParserItemNotExistsException(string key)
            : base($"Key \"{key}\" does not exists in command")
        {
            Key = key;
        }
        
    }
}