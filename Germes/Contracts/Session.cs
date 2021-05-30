using Germes.Domain.Data.Models;

namespace Germes.Domain.Data
{
    /// <summary>
    ///     Сессия
    /// </summary>
    public class Session
    {
        /// <summary>
        ///     Пользователь
        /// </summary>
        public UserModel User{ get; set; }
    }
}
