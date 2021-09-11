namespace Germes.Accountant.Domain.Models
{
    /// <summary>
    ///     Категория рассходов
    /// </summary>
    public class ExpenseCategory
    {
        /// <summary>
        ///     Имя категории
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Описание категории
        /// </summary>
        public string Description { get; set; }
    }
}
