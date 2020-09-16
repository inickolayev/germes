using Germes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Services
{
    public class BotService : IBotService
    {
        public async Task<BotResult> HandleNewMessageAsync(BotMessage message, CancellationToken token)
        {
            return new BotResult
            {
                Text = "Принял!"
            };
        }
    }
}
