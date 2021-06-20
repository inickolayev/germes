using System;

namespace Germes.Domain.Data.Models
{
    /// <summary>
    ///     Модель рассхода
    /// </summary>
    public class Expense
    {
        /// <summary>
        ///     Трата
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        ///     Дата создания расхода
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        ///     Категория рассхода
        /// </summary>
        public ExpenseCategory Category { get; set; }
        /// <summary>
        ///     Подкатегория рассходов
        /// </summary>
        public ExpenseSubCategory SubCategory { get; set; }
    }
}
