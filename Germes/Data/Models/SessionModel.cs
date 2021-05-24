using Germes.Data.Models;
using Germes.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Models
{
    /// <summary>
    ///     Сессия
    /// </summary>
    public class SessionModel
    {
        /// <summary>
        ///     Пользователь
        /// </summary>
        public UserModel User { get; set; }
        /// <summary>
        ///     Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        ///     Идентификатор чата с пользователем
        /// </summary>
        public string ChatId { get; set; }
    }
}
