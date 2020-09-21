using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Results
{
    /// <summary>
    ///     Общий класс ошибок
    /// </summary>
    public abstract class AbstractError
    {
        public string Message { get; set; }

        public AbstractError(string message)
        {
            Message = message;
        }
    }
}
