﻿using System;
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
        private readonly List<CommandKey> keys = new List<CommandKey>();
        
        private const string ItemPattern = "{\\??[a-zA-Z]*}";
        private static readonly string DefaultValue = string.Empty;

        public CommandParser(string pattern)
        {
            _pattern = pattern;
            var matches = new Regex(_pattern).Matches(ItemPattern);
            foreach (Match match in matches)
            {
                var matchKey = match.Value;
                var key = matchKey.Remove('{').Remove('}');
                bool isOptional = key[0] == '?';
                if (isOptional)
                {
                    key = key.Remove('?');
                    var itemKey = new CommandKey(key, true);
                    keys.Add(itemKey);
                }
                else
                {
                    var itemKey = new CommandKey(key, false);
                    keys.Add(itemKey);
                }
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
            int currentId = 0;
            var items = new List<CommandItem>();
            foreach (var itemKey in keys)
            {
                var key = itemKey.Key;
                bool isOptional = key[0] == '?';
                if (isOptional)
                {
                    var value = values.Length > currentId
                        ? values[currentId]
                        : DefaultValue;
                    var item = new CommandItem(key, value, true);
                    items.Add(item);
                }
                else
                {
                    var value = values.Length > currentId
                        ? values[currentId]
                        : throw new CommandParserItemNotExistsException(key, command);
                    var item = new CommandItem(key, value, false);
                    items.Add(item);
                }
            }
            return new CommandItems(items);
        }

        class CommandKey
        {
            public string Key { get; set; }
            public bool IsOptional { get; }

            public CommandKey(string key, bool isOptional = false)
            {
                Key = key;
                IsOptional = isOptional;
            }
        }
    }
}