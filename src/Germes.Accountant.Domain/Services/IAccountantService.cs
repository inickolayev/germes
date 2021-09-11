using System.Threading;
using System.Threading.Tasks;

namespace Germes.Accountant.Domain.Services
{
    public interface IAccountantService
    {
        Task<decimal> GetBalance(string categoryName, CancellationToken cancellationToken);
    }
}