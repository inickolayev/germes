using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Domain.Data;

namespace Germes.Abstractions.Plugins
{
    /// <summary>
    ///     Плагин обработки сообщений
    /// </summary>
    public interface IBotPlugin
    {
        public bool IsAllow { get; }

        Task<bool> CheckAsync(BotMessage message, CancellationToken token = default);
        Task<PluginResult> Handle(BotMessage message, CancellationToken token = default);
        string GetHelpDescription();
    }
}
