using Germes.Data;
using Germes.Data.Results;
using Germes.Services;
using Germes.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;

namespace Germes.Test.Mocks
{
    public class PluginServiceMock : IPluginService, IDisposable
    {
        private readonly List<IBotPlugin> _plagins = new List<IBotPlugin>();

        public static readonly Dictionary<Session, List<IBotPlugin>> PluginsDb = new Dictionary<Session, List<IBotPlugin>>();

        public IEnumerable<IBotPlugin> DefaultPlugins { get; }

        public PluginServiceMock(ISessionManager sessionManager, IAccountantService accountantService, ICategoryService categoryService)
        {
            var session = sessionManager.CurrentSession;
            DefaultPlugins = new List<IBotPlugin>
            {
                new AccountantPlugin(sessionManager, accountantService, categoryService)
            };

            if (!PluginsDb.ContainsKey(session))
                PluginsDb.Add(session, DefaultPlugins.ToList());
            _plagins = PluginsDb[session];
        }

        public async Task<OperationResult<IEnumerable<IBotPlugin>>> GetPluginsAsync(CancellationToken token)
            => new OperationResult<IEnumerable<IBotPlugin>>(_plagins);

        public async Task<OperationResult> AddPluginAsync(IBotPlugin plugin, CancellationToken token)
        {
            _plagins.Add(plugin);
            return new OperationResult();
        }

        public void Dispose()
        {
            PluginsDb.Clear();
        }
    }
}
