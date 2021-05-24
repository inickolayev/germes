using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Services
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public class AccountantService : IAccountantService
    {
        private readonly SessionModel _session;
        private readonly List<ExpenseModel> _expenses = new List<ExpenseModel>();
        private readonly List<IncomeModel> _incomes = new List<IncomeModel>();

        /// <summary>
        ///     Рассходы
        /// </summary>
        private static readonly Dictionary<SessionModel, List<ExpenseModel>> _expensesDb = new Dictionary<SessionModel, List<ExpenseModel>>();
        /// <summary>
        ///     Доходы
        /// </summary>
        private static readonly Dictionary<SessionModel, List<IncomeModel>> _incomesDb = new Dictionary<SessionModel, List<IncomeModel>>();

        public AccountantService(ISessionManager sessionManager)
        {
            _session = sessionManager.CurrentSession;
            if (!_expensesDb.ContainsKey(_session))
                _expensesDb.Add(_session, new List<ExpenseModel>());
            _expenses = _expensesDb[_session];
            if (!_incomesDb.ContainsKey(_session))
                _incomesDb.Add(_session, new List<IncomeModel>());
            _incomes = _incomesDb[_session];
        }

        /// <summary>
        ///     Добавить расход
        /// </summary>
        /// <param name="expense">Расход</param>
        public async Task<OperationResult<ExpenseModel>> AddAsync(ExpenseModel expense, CancellationToken token)
        {
            _expenses.Add(expense);
            return new OperationResult<ExpenseModel>(expense);
        }
        /// <summary>
        ///     Добавить доход
        /// </summary>
        /// <param name="income">Доход</param>
        public async Task<OperationResult<IncomeModel>> AddAsync(IncomeModel income, CancellationToken token)
        {
            _incomes.Add(income);
            return new OperationResult<IncomeModel>(income);
        }
        /// <summary>
        ///     Посчитать баланс (остаток)
        /// </summary>
        public  async Task<OperationResult<decimal>> GetBalanceAsync(CancellationToken token)
        {
            var result = _incomes.Select(inc => inc.Cost).Sum() - _expenses.Select(inc => inc.Cost).Sum();
            return new OperationResult<decimal>(result);
        }
    }
}
