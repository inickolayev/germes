using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Plugins;
using Germes.Abstractions.Services;

namespace Germes.Implementations.Services
{
    public class PluginService : IPluginService
    {
        private readonly IEnumerable<IBotPlugin> _plugins;

        public PluginService(IEnumerable<IBotPlugin> plugins)
        {
            _plugins = plugins;
        }

        public Task<IEnumerable<IBotPlugin>> GetPluginsAsync(CancellationToken token)
            => Task.FromResult(_plugins);
    }
}
