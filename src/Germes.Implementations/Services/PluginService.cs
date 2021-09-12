using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Services;
using Germes.Domain.Plugins;

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
