using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Models
{
    /// <summary>
    ///     Подкатегория рассходов
    /// </summary>
    public class ExpenseSubCategoryModel
    {
        /// <summary>
        ///     Родительская категория
        /// </summary>
        public ExpenseCategoryModel ParentCategory { get; set; }
        /// <summary>
        ///     Имя подкатегории
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Описание подкатегории
        /// </summary>
        public string Description { get; set; }
    }
}
