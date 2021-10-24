using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Plugins;

namespace Germes.Abstractions.Services
{
    /// <summary>
    ///     Сервис для работы с плагинами
    /// </summary>
    public interface IPluginService
    {
        /// <summary>
        ///     Получить все плагины
        /// </summary>
        Task<IEnumerable<IBotPlugin>> GetPluginsAsync(CancellationToken token);
    }
}
