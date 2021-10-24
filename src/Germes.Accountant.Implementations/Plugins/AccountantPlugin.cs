using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Abstractions.Services;
using Germes.Accountant.Domain.Services;
using Germes.Accountant.Implementations.Translations;
using Germes.Data;
using Germes.Domain.Data;
using Germes.Domain.Plugins;
using Germes.Implementations.Plugins;
using Germes.User.Contracts.Inbound;
using Germes.User.Domain.Services;
using Inbound = Germes.Accountant.Contracts.Inbound;

namespace Germes.Accountant.Implementations.Plugins
{
    /// <summary>
    ///     Плагин, помогающий подсчитывать рассходы
    /// </summary>
    public class AccountantPlugin : CommonBotPlugin
    {
        public override bool IsAllow => true;

        public AccountantPlugin(IServiceProvider serviceProvider)
            : base(
                new AddTransactionCommand(serviceProvider),
                new GetBalanceCommand(serviceProvider)
            )
        {
        }
    }
}
