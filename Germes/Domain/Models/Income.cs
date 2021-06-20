using System;

namespace Germes.Domain.Data.Models
{
    /// <summary>
    ///     Модель дохода
    /// </summary>
    public class Income
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
        public IncomeCategory Category { get; set; }
    }
}
