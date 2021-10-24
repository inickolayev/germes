using System.Threading;
using System.Threading.Tasks;

namespace Germes.Abstractions.Services
{
    public interface ISourceAdapter
    {
        bool Check(string sourceId);
        Task<string> GetName(string chatId, CancellationToken cancellationToken);
    }
}