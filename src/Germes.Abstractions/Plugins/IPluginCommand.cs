using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results;
using Germes.Domain.Data;

namespace Germes.Abstractions.Plugins
{
    public interface IPluginCommand
    {
        bool Check(BotMessage message);
        Task<PluginResult> Handle(BotMessage message, CancellationToken token);
        string GetHelpDescription();
    }
}