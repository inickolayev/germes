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
    public class AccountantService
    {
        /// <summary>
        ///     Рассходы
        /// </summary>
        private readonly ConcurrentDictionary<Session, List<ExpenseModel>> _expenses = new ConcurrentDictionary<Session, List<ExpenseModel>>();
        /// <summary>
        ///     Доходы
        /// </summary>
        private readonly ConcurrentDictionary<Session, List<IncomeModel>> _incomes = new ConcurrentDictionary<Session, List<IncomeModel>>();

        /// <summary>
        ///     Добавить расход
        /// </summary>
        /// <param name="expense">Расход</param>
        public async Task<OperationResult<ExpenseModel>> AddAsync(Session session, ExpenseModel expense, CancellationToken token)
        {
            var expenses = _expenses.GetOrAdd(session, new List<ExpenseModel>());
            expenses.Add(expense);
            return new OperationResult<ExpenseModel>(expense);
        }
        /// <summary>
        ///     Добавить доход
        /// </summary>
        /// <param name="income">Доход</param>
        public async Task<OperationResult<IncomeModel>> AddAsync(Session session, IncomeModel income, CancellationToken token)
        {
            var incomes = _incomes.GetOrAdd(session, new List<IncomeModel>());
            incomes.Add(income);
            return new OperationResult<IncomeModel>(income);
        }
        /// <summary>
        ///     Посчитать баланс (остаток)
        /// </summary>
        public  async Task<OperationResult<decimal>> GetBalanceAsync(Session session, CancellationToken token)
        {
            var expenses = _expenses.GetOrAdd(session, new List<ExpenseModel>());
            var incomes = _incomes.GetOrAdd(session, new List<IncomeModel>());
            var result = _incomes[session].Select(inc => inc.Cost).Sum() - _expenses[session].Select(inc => inc.Cost).Sum();
            return new OperationResult<decimal>(result);
        }
    }
}
