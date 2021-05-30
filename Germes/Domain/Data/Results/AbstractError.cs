namespace Germes.Domain.Data.Results
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
