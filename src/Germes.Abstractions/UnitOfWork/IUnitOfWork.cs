using System.Threading;
using System.Threading.Tasks;

namespace Germes.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task Complete(CancellationToken ct);
    }
}