using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;

namespace Germes.Accountant.Domain.Repositories
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public interface IAccountantRegisterRepository
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
    }
}
