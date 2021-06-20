using Germes.Domain.Data;
using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Implementations.Services
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public class AccountantRepository : IAccountantRepository
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

        public AccountantRepository(ISessionManager sessionManager)
        {
            _session = sessionManager.CurrentSession;
            if (!_expensesDb.ContainsKey(_session))
                _expensesDb.Add(_session, new List<Expense>());
            _expenses = _expensesDb[_session];
            if (!_incomesDb.ContainsKey(_session))
                _incomesDb.Add(_session, new List<Income>());
            _incomes = _incomesDb[_session];
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
        public  async Task<decimal> GetBalanceAsync(CancellationToken token)
        {
            var result = _incomes.Select(inc => inc.Cost).Sum() - _expenses.Select(inc => inc.Cost).Sum();
            return result;
        }
    }
}
