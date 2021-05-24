using Germes.Data;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Extensions;
using Germes.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Germes.Test.Plugins
{
    public class AccountantTests : AbstractPluginTests
    {
        public AccountantTests(
            IUserService userService,
            ISessionService sessionService,
            IServiceProvider serviceProvider
        ) : base(userService, sessionService, serviceProvider)
        {
        }

        [Fact]
        public async Task AddExpense_success()
        {
            await AddExpenseAsync(100, "Продукты");
            var req = CreateNewMessage("Продукты 500");
            var expected = "Остаток: -500 руб.";

            var res = await MSendSafe<RequestNewMessage, BotResult>(req);

            Assert.True(res.IsSuccess);
            Assert.Equal(expected, res.Result.Text);
        }

        [Fact]
        public async Task AddIncome_success()
        {
            var req = CreateNewMessage("ЗП 5000");
            var expected = "Остаток: 5000 руб.";

            //var res = await _mediator.SendSafe<RequestNewMessage, BotResult>(req);

            //Assert.True(res.IsSuccess);
            //Assert.Equal(expected, res.Result.Text);
        }
    }
}
