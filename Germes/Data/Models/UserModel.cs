using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Models
{
    public class UserModel
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
