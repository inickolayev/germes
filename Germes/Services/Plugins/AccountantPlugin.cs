using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Services.Plugins
{
    /// <summary>
    ///     Плагин, помогающий подсчитывать рассходы
    /// </summary>
    public class AccountantPlugin : IBotPlugin
    {
        private readonly AccountantService _service;

        public bool IsAllow { get; set; }

        public AccountantPlugin(AccountantService service)
        {
            _service = service;
        }

        public async Task<OperationResult<bool>> CheckAsync(BotMessage message, CancellationToken token)
        {
            var text = message.Text;
            var args = text.Split(" ");
            var result = args.Count() == 2 && decimal.TryParse(args[1], out var _);
            return new OperationResult<bool>(result);
        }

        public async Task<OperationResult<BotResult>> HandleAsync(Session session, BotMessage message, CancellationToken token)
        {
            // TODO: изменить логику работы
            // логика сейчас наиболее простая: парсим строку, берем второе слово и читаем из него расход
            var text = message.Text;
            var cost = Convert.ToDecimal(text.Split(' ')[1]);
            var expense = new ExpenseModel
            {
                Cost = cost,
                Date = DateTime.Now
            };

            var expenseAddRes = await _service.AddAsync(session, expense, token);
            if (!expenseAddRes.IsSuccess)
                return expenseAddRes.To<BotResult>();
            var balanceRes = await _service.GetBalanceAsync(session, token);
            if (!balanceRes.IsSuccess)
                return balanceRes.To<BotResult>();

            var result = $"Остаток: {balanceRes.Result}";
            return new OperationResult<BotResult>(new BotResult
            {
                Text = result
            });
        }
    }
}
