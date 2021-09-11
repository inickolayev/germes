using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;

namespace Germes.Accountant.Implementations.Repositories
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public class AccountantReadRepository : IAccountantReadRepository
    {
        private readonly List<Expense> _expenses = new List<Expense>();
        private readonly List<Income> _incomes = new List<Income>();

        public AccountantReadRepository()
        {
        }

        /// <summary>
        ///     Добавить расход
        /// </summary>
        /// <param name="expense">Расход</param>
        public async Task<Expense> AddAsync(Expense expense, CancellationToken token)
        {
            _expenses.Add(expense);
            return expense;
        }
        /// <summary>
        ///     Добавить доход
        /// </summary>
        /// <param name="income">Доход</param>
        public async Task<Income> AddAsync(Income income, CancellationToken token)
        {
            _incomes.Add(income);
            return income;
        }
        /// <summary>
        ///     Посчитать баланс (остаток)
        /// </summary>
        public async Task<decimal> GetBalanceAsync(string categoryName, CancellationToken token)
        {
            var incomeSum = _incomes.Where(inc =>
                    string.IsNullOrWhiteSpace(categoryName)
                    || inc.Category.Name == categoryName)
                .Select(inc => inc.Cost).Sum();
            var expenseSum = _expenses.Where(exp =>
                    string.IsNullOrWhiteSpace(categoryName)
                    || exp.Category.Name == categoryName)
                .Select(exp => exp.Cost).Sum();
            var result = expenseSum - incomeSum;
            return result;
        }
    }
}
