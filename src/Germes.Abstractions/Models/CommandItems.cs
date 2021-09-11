using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Germes.Domain.Exceptions;

namespace Germes.Abstractions.Models
{
    public class CommandItems
    {
        private readonly Dictionary<string, CommandItem> _items;
        
        public CommandItems(IEnumerable<CommandItem> items)
        {
            _items = items.ToDictionary(it => it.Key, it => it);
        }
        
        public int GetInt(string key)
        {
            var stringValue = GetString(key);
            return int.Parse(stringValue);
        }
        
        public decimal GetDecimal(string key)
        {
            var stringValue = GetString(key);
            return Decimal.Parse(stringValue);
        }

        public string GetString(string key)
        {
            if (!_items.ContainsKey(key))
            {
                throw new CommandParserItemNotExistsException(key);
            }

            var item = _items[key];
            return item.Value;
        }
    }

    public class CommandItem
    {
        public bool IsOptional { get; }
        public string Key { get; }
        public string Value { get; }

        public CommandItem(string key, string value, bool isOptional = false)
        {
            Key = key;
            Value = value;
            IsOptional = isOptional;
        }
    }
}