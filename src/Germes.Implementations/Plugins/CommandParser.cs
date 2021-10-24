using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Germes.Abstractions.Models;
using Germes.Abstractions.Services;
using Germes.Domain.Exceptions;

namespace Germes.Implementations.Plugins
{
    public class CommandParser : ICommandParser
    {
        private readonly string _pattern;
        private const string ConstPattern = "\\A[^{} ]+\\Z";
        private const string ValuePattern = "{\\??[a-zA-Z]+}";
        private static readonly string ItemPattern = string.Join("|", new[] {ConstPattern, ValuePattern});
        private static readonly string DefaultValue = string.Empty;

        public readonly List<CommandKey> Keys = new List<CommandKey>();

        public CommandParser(string pattern)
        {
            _pattern = pattern;
            var matches = new Regex(ItemPattern).Matches(_pattern);
            foreach (Match match in matches)
            {
                TryAddConstItem(match);
                TryAddValueItem(match);
            }
        }

        private void TryAddConstItem(Match match)
        {
            var matchKey = match.Value;
            var isMatch = new Regex(ConstPattern).IsMatch(matchKey);
            if (!isMatch) return;
            
            var itemKey = new CommandKey(matchKey, isOptional: false, isConst: true);
            Keys.Add(itemKey);
        }

        private void TryAddValueItem(Match match)
        {
            var matchKey = match.Value;
            var isMatch = new Regex(ValuePattern).IsMatch(matchKey);
            if (!isMatch) return;
            
            var key = matchKey.Replace("{", "").Replace("}", "");
            bool isOptional = key[0] == '?';
            if (isOptional)
            {
                key = key.Replace("?", "");
                var itemKey = new CommandKey(key, true);
                Keys.Add(itemKey);
            }
            else
            {
                var itemKey = new CommandKey(key);
                Keys.Add(itemKey);
            }
        }
        
        public bool Contains(string command)
        {
            try
            {
                var result = Parse(command);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public CommandItems Parse(string command)
        {
            var values = command
                .Split(" ")
                .Where(it => !string.IsNullOrWhiteSpace(it))
                .ToArray();
            
            if (values.Length > Keys.Count())
            {
                throw new CommandParserCommandNotValidException(command, _pattern);
            }
            
            int currentIndex = 0;
            var items = new List<CommandItem>();
            foreach (var itemKey in Keys)
            {
                var key = itemKey.Key;
                if (itemKey.IsConst)
                {
                    var value = values.Length > currentIndex
                        ? values[currentIndex]
                        : throw new CommandParserItemNotExistsException(key, command);
                    if (value != key)
                    {
                        throw new CommandParserItemNotExistsException(key, command);
                    }
                }
                else
                {
                    if (itemKey.IsOptional)
                    {
                        var value = values.Length > currentIndex
                            ? values[currentIndex]
                            : DefaultValue;
                        var item = new CommandItem(key, value, true);
                        items.Add(item);
                    }
                    else
                    {
                        var value = values.Length > currentIndex
                            ? values[currentIndex]
                            : throw new CommandParserItemNotExistsException(key, command);
                        var item = new CommandItem(key, value, false);
                        items.Add(item);
                    }
                }

                currentIndex++;
            }

            return new CommandItems(items);
        }

        public class CommandKey
        {
            public string Key { get; }
            public bool IsOptional { get; }
            public bool IsConst { get; }

            public CommandKey(string key, bool isOptional = false, bool isConst = false)
            {
                Key = key;
                IsOptional = isOptional;
                IsConst = isConst;
            }
        }
    }
}