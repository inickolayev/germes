using Germes.Domain.Data;
using Germes.Domain.Data.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Domain;
using Germes.Domain.Plugins;
using Germes.Implementations.Services;

namespace Germes.Implementations.Plugins
{
    public class PluginService : IPluginService
    {
        private readonly List<IBotPlugin> _plugins;

        private static readonly Dictionary<Session, List<IBotPlugin>> _plaginsDb = new Dictionary<Session, List<IBotPlugin>>();

        public IEnumerable<IBotPlugin> DefaultPlugins { get; }

        public PluginService(ISessionManager sessionManager, IAccountantService accountantService, ICategoryRepository categoryRepository)
        {
            var session = sessionManager.CurrentSession;
            DefaultPlugins = new List<IBotPlugin>
            {
                new AccountantPlugin(sessionManager, accountantService, categoryRepository)
            };

            if (!_plaginsDb.ContainsKey(session))
                _plaginsDb.Add(session, DefaultPlugins.ToList());
            _plugins = _plaginsDb[session];
        }

        public async Task<OperationResult<IEnumerable<IBotPlugin>>> GetPluginsAsync(CancellationToken token)
            => new OperationResult<IEnumerable<IBotPlugin>>(_plugins);

        public async Task<OperationResult> AddPluginAsync(IBotPlugin plugin, CancellationToken token)
        {
            _plugins.Add(plugin);
            return new OperationResult();
        }
    }
}
