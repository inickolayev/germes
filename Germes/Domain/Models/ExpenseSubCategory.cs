namespace Germes.Domain.Data.Models
{
    /// <summary>
    ///     Подкатегория рассходов
    /// </summary>
    public class ExpenseSubCategory
    {
        /// <summary>
        ///     Родительская категория
        /// </summary>
        public ExpenseCategory ParentCategory { get; set; }
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
