using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Models
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
        ///     Категория рассхода
        /// </summary>
        public ExpenseCategoryModel Category { get; set; }
        /// <summary>
        ///     Подкатегория рассходов
        /// </summary>
        public ExpenseSubCategoryModel SubCategory { get; set; }
    }
}
