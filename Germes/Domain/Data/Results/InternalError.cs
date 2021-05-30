using System;

namespace Germes.Domain.Data.Results
{
    /// <summary>
    ///     Внутрняя неожиданная ошибка
    /// </summary>
    public class InternalError : AbstractError
    {
        public Exception Exception { get; set; }

        public InternalError(string message, Exception exception = default)
            : base(message)
        {
            Exception = exception;
        }
    }
}
