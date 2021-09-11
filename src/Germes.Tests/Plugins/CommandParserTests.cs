using System.Collections.Generic;
using System.Linq;
using Germes.Implementations.Plugins;
using NUnit.Framework;

namespace Germes.Tests.Plugins
{
    public class CommandParserTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Check()
        {
            var commandParser = new CommandParser("{cost}{?comment}{?category}");
            Compare(new[]
            {
                new CommandParser.CommandKey("cost", false),
                new CommandParser.CommandKey("comment", true),
                new CommandParser.CommandKey("category", true),
            }, commandParser.Keys);
        }
        
        private static void Compare(IEnumerable<CommandParser.CommandKey> expectedKeys,
            IEnumerable<CommandParser.CommandKey> commandKeys)
        {
            Assert.AreEqual(expectedKeys.Count(), commandKeys.Count());
            var commandKeysList = commandKeys.ToList();
            int commandId = 0;
            foreach (var expectedKey in expectedKeys)
            {
                var commandKey = commandKeysList[commandId];
                Assert.AreEqual(expectedKey.Key, commandKey.Key);
                Assert.AreEqual(expectedKey.IsOptional, commandKey.IsOptional);
                commandId++;
            }
        }
    }
}