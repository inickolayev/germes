using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Results;
using Germes.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Test.Mocks
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public class AccountantServiceMock : IAccountantService, IDisposable
    {
        private readonly Session _session;
        private readonly List<ExpenseModel> _expenses = new List<ExpenseModel>();
        private readonly List<IncomeModel> _incomes = new List<IncomeModel>();

        /// <summary>
        ///     Рассходы
        /// </summary>
        public static readonly Dictionary<Session, List<ExpenseModel>> ExpensesDb = new Dictionary<Session, List<ExpenseModel>>();
        /// <summary>
        ///     Доходы
        /// </summary>
        public static readonly Dictionary<Session, List<IncomeModel>> IncomesDb = new Dictionary<Session, List<IncomeModel>>();

        public AccountantServiceMock(ISessionManager sessionManager)
        {
            _session = sessionManager.CurrentSession;
            if (!ExpensesDb.ContainsKey(_session))
                ExpensesDb.Add(_session, new List<ExpenseModel>());
            _expenses = ExpensesDb[_session];
            if (!IncomesDb.ContainsKey(_session))
                IncomesDb.Add(_session, new List<IncomeModel>());
            _incomes = IncomesDb[_session];
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

        public void Dispose()
        {
            ExpensesDb.Clear();
            IncomesDb.Clear();
        }
    }
}
