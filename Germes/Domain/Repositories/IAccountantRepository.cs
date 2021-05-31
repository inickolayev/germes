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
        Task<OperationResult<ExpenseModel>> AddAsync(ExpenseModel expense, CancellationToken token);
        /// <summary>
        ///     Добавить доход
        /// </summary>
        /// <param name="income">Доход</param>
        Task<OperationResult<IncomeModel>> AddAsync(IncomeModel income, CancellationToken token);
        /// <summary>
        ///     Посчитать баланс (остаток)
        /// </summary>
        Task<OperationResult<decimal>> GetBalanceAsync(CancellationToken token);
    }
}
