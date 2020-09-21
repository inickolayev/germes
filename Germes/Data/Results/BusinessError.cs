using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Results
{
    /// <summary>
    ///     Бизнес ошибки
    /// </summary>
    public class BusinessError : AbstractError
    {
        public string Detail { get; set; }

        public BusinessError(string message, string detail = default)
            : base(message)
        {
            Detail = detail;
        }
    }
}
