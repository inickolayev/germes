using System;

namespace Germes.Abstractions.Models.Results
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
