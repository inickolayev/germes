using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models;
using Germes.Abstractions.Models.Results;
using Germes.Data;
using Germes.Domain.Data;

namespace Germes.Abstractions.Plugins
{
    public interface ICommandHandler
    {
        Task<PluginResult> Handle(BotMessage message, CommandItems commandItems, CancellationToken cancellationToken);
    }
}