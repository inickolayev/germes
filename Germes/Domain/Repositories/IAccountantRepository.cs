using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Implementations.Services
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public interface IAccountantRepository
    {
        /// <summary>
        ///     Добавить расход
        /// </summary>
        /// <param name="expense">Расход</param>
        Task<Expense> AddAsync(Expense expense, CancellationToken token);
        /// <summary>
        ///     Добавить доход
        /// </summary>
        /// <param name="income">Доход</param>
        Task<Income> AddAsync(Income income, CancellationToken token);
        /// <summary>
        ///     Посчитать баланс (остаток)
        /// </summary>
        Task<decimal> GetBalanceAsync(CancellationToken token);
    }
}
