﻿using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Germes.Domain
{
    /// <summary>
    ///     Репозиторий по работе с категориями
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        ///     Получить категорию расходов по наименованию (без учета регистра)
        /// </summary>
        /// <param name="category">Имя категории</param>
        Task<ExpenseCategory> GetExpenseCategoryAsync(string category, CancellationToken token = default);
        /// <summary>
        ///     Добавить категории расходов
        /// </summary>
        /// <param name="category">Имя категории</param>
        /// <param name="description">Описание категории</param>
        Task<ExpenseCategory> AddExpenseCategoryAsync(string category, string description = default, CancellationToken token = default);
        
        /// <summary>
        ///     Получить категорию доходов по наименованию (без учета регистра)
        /// </summary>
        /// <param name="category">Имя категории</param>
        Task<IncomeCategory> GetIncomeCategoryAsync(string category, CancellationToken token);
        /// <summary>
        ///     Добавить категории доходов
        /// </summary>
        /// <param name="category">Имя категории</param>
        /// <param name="description">Описание категории</param>
        Task<IncomeCategory> AddIncomeCategoryAsync(string category, string description = default, CancellationToken token = default);
    }
}
