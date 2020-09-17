using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Models
{
    /// <summary>
    ///     Категория доходов
    /// </summary>
    public class IncomeCategoryModel
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
