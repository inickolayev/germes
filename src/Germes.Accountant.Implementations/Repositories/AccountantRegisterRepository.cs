using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;

namespace Germes.Accountant.Implementations.Repositories
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public class AccountantRegisterRepository : IAccountantRegisterRepository
    {
        private readonly Session _session;
        private readonly List<Expense> _expenses = new List<Expense>();
        private readonly List<Income> _incomes = new List<Income>();

        /// <summary>
        ///     Рассходы
        /// </summary>
        private static readonly Dictionary<Session, List<Expense>> _expensesDb = new Dictionary<Session, List<Expense>>();
        /// <summary>
        ///     Доходы
        /// </summary>
        private static readonly Dictionary<Session, List<Income>> _incomesDb = new Dictionary<Session, List<Income>>();

        public AccountantRegisterRepository()
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
    }
}
