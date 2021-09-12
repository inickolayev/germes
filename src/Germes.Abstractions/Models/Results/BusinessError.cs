namespace Germes.Abstractions.Models.Results
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
