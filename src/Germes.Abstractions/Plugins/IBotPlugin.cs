using Germes.Domain.Data;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;

namespace Germes.Domain.Plugins
{
    /// <summary>
    ///     Плагин обработки сообщений
    /// </summary>
    public interface IBotPlugin
    {
        public bool IsAllow { get; }

        Task<bool> CheckAsync(BotMessage message, CancellationToken token = default);
        Task<PluginResult> Handle(BotMessage message, CancellationToken token = default);
    }
}
