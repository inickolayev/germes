using Germes.Data;
using Germes.Data.Results;
using Germes.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;

namespace Germes.Services
{
    public class PluginService : IPluginService
    {
        private readonly List<IBotPlugin> _plagins = new List<IBotPlugin>();

        private static readonly Dictionary<Session, List<IBotPlugin>> _plaginsDb = new Dictionary<Session, List<IBotPlugin>>();

        public IEnumerable<IBotPlugin> DefaultPlugins { get; }

        public PluginService(ISessionManager sessionManager, IAccountantService accountantService, ICategoryService categoryService)
        {
            var session = sessionManager.CurrentSession;
            DefaultPlugins = new List<IBotPlugin>
            {
                new AccountantPlugin(sessionManager, accountantService, categoryService)
            };

            if (!_plaginsDb.ContainsKey(session))
                _plaginsDb.Add(session, DefaultPlugins.ToList());
            _plagins = _plaginsDb[session];
        }

        public async Task<OperationResult<IEnumerable<IBotPlugin>>> GetPluginsAsync(CancellationToken token)
            => new OperationResult<IEnumerable<IBotPlugin>>(_plagins);

        public async Task<OperationResult> AddPluginAsync(IBotPlugin plugin, CancellationToken token)
        {
            _plagins.Add(plugin);
            return new OperationResult();
        }
    }
}
