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
    public interface IAccountantService
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
