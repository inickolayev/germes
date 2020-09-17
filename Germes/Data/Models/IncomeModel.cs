using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Models
{
    /// <summary>
    ///     Модель дохода
    /// </summary>
    public class IncomeModel
    {
        /// <summary>
        ///     Доход
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        ///     Дата создания дохода
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        ///     Категория дохода
        /// </summary>
        public IncomeCategoryModel Category { get; set; }
    }
}
