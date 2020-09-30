using Germes.Data;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Extensions;
using MediatR;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Germes.Test.Plugins
{
    public class AccountantTests : AbstractPluginTests
    {
        private readonly IMediator _mediator;

        public AccountantTests(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Fact]
        public async Task AddExpense_success()
        {
            var req = CreateNewMessage("Продукты 500");
            var expected = "Остаток: -500 руб.";

            var res = await _mediator.SendSafe<RequestNewMessage, BotResult>(req);

            Assert.True(res.IsSuccess);
            Assert.Equal(expected, res.Result.Text);
        }
    }
}
