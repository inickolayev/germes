using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;

namespace Germes.Accountant.Domain.Repositories
{
    /// <summary>
    ///     Репозиторий по работе с категориями
    /// </summary>
    public interface ICategoryRegisterRepository
    {
        /// <summary>
        ///     Добавить категории расходов
        /// </summary>
        /// <param name="category">Имя категории</param>
        /// <param name="description">Описание категории</param>
        Task<ExpenseCategory> AddExpenseCategoryAsync(string category, string description = default, CancellationToken token = default);
        
        /// <summary>
        ///     Добавить категории доходов
        /// </summary>
        /// <param name="category">Имя категории</param>
        /// <param name="description">Описание категории</param>
        Task<IncomeCategory> AddIncomeCategoryAsync(string category, string description = default, CancellationToken token = default);
    }
}
