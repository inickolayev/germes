using System.Collections.Generic;
using System.Linq;
using Germes.Domain.Exceptions;
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
        public void InitKeys_Success()
        {
            var commandParser = new CommandParser("{cost}{comment}{?category}");
            Compare(new[]
            {
                new CommandParser.CommandKey("cost", false),
                new CommandParser.CommandKey("comment", false),
                new CommandParser.CommandKey("category", true),
            }, commandParser.Keys);
        }
        
        [TestCase("125 еда макдак", true)]
        [TestCase("125 еда", true)]
        [TestCase("что-то еда", true)]
        [TestCase("еда 125", true)]
        [TestCase("125", false)]
        [TestCase("что это такое происходит", false)]
        public void ContainsPatternRequest_Success(string command, bool expectedContains)
        {
            var commandParser = new CommandParser("{cost}{category}{?comment}");
            var contains = commandParser.Contains(command);
            
            Assert.AreEqual(expectedContains, contains);            
        }
        
        [Test]
        public void ParseFoodFirstRequest_Success()
        {
            var commandParser = new CommandParser("{cost}{category}{?comment}");
            var parseResult = commandParser.Parse("125 еда макдак");
            var cost = parseResult.GetDecimal("cost");
            var category = parseResult.GetString("category");
            var comment = parseResult.GetString("comment");
            
            Assert.AreEqual(cost, 125);
            Assert.AreEqual(category, "еда");
            Assert.AreEqual(comment, "макдак");
        }
        
        [Test]
        public void ParseFoodSecondRequest_Success()
        {
            var commandParser = new CommandParser("{cost}{category}{?comment}");
            var parseResult = commandParser.Parse("125 еда");
            var cost = parseResult.GetDecimal("cost");
            var category = parseResult.GetString("category");
            var comment = parseResult.GetString("comment");
            
            Assert.AreEqual(cost, 125);
            Assert.AreEqual(category, "еда");
            Assert.IsEmpty(comment);
        }
        
        [Test]
        public void ParseFoodRequest_Error()
        {
            var commandParser = new CommandParser("{cost}{category}{?comment}");
            
            Assert.Throws<CommandParserItemNotExistsException>(() =>
            {
                commandParser.Parse("125");
            });
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