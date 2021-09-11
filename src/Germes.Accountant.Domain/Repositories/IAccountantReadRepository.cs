using System.Threading;
using System.Threading.Tasks;

namespace Germes.Accountant.Domain.Repositories
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public interface IAccountantReadRepository
    {
        /// <summary>
        ///     Посчитать баланс (остаток)
        /// </summary>
        Task<decimal> GetBalanceAsync(string categoryName, CancellationToken token);
    }
}
