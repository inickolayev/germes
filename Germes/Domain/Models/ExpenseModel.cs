using System;

namespace Germes.Domain.Data.Models
{
    /// <summary>
    ///     Модель рассхода
    /// </summary>
    public class ExpenseModel
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
        public ExpenseCategoryModel Category { get; set; }
        /// <summary>
        ///     Подкатегория рассходов
        /// </summary>
        public ExpenseSubCategoryModel SubCategory { get; set; }
    }
}
