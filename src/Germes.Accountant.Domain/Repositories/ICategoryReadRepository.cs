using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;

namespace Germes.Accountant.Domain.Repositories
{
    /// <summary>
    ///     Репозиторий по работе с категориями
    /// </summary>
    public interface ICategoryReadRepository
    {
        /// <summary>
        ///     Получить категорию расходов по наименованию (без учета регистра)
        /// </summary>
        /// <param name="category">Имя категории</param>
        Task<ExpenseCategory> GetExpenseCategoryAsync(string category, CancellationToken token = default);
        
        /// <summary>
        ///     Получить категорию доходов по наименованию (без учета регистра)
        /// </summary>
        /// <param name="category">Имя категории</param>
        Task<IncomeCategory> GetIncomeCategoryAsync(string category, CancellationToken token);
    }
}
