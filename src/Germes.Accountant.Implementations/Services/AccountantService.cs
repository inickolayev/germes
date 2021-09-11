using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Repositories;
using Germes.Accountant.Domain.Services;

namespace Germes.Accountant.Implementations.Services
{
    public class AccountantService : IAccountantService
    {
        private readonly IAccountantReadRepository _accountantReadRepository;

        public AccountantService(IAccountantReadRepository accountantReadRepository)
        {
            _accountantReadRepository = accountantReadRepository;
        }
        
        public async Task<decimal> GetBalance(string categoryName, CancellationToken cancellationToken)
            => await _accountantReadRepository.GetBalanceAsync(categoryName, cancellationToken);
    }
}