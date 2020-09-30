using Germes.Data;
using Germes.Data.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Germes.Test.Plugins
{
    public abstract class AbstractPluginTests
    {
        protected RequestNewMessage CreateNewMessage(string text, string chatId = TestConsts.ChatIdDefault)
           => new RequestNewMessage
           {
               Message = new BotMessage
               {
                   ChatId = chatId,
                   Text = text
               }
           };
    }
}
