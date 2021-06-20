namespace Germes.Domain.Data.Models
{
    public class User
    {
        /// <summary>
        ///     Идентификатор чата с пользователем
        /// </summary>
        public string ChatId { get; set; }

        /// <summary>
        ///     Имя пользователя
        /// </summary>
        public string Name { get; set; }
    }
}
