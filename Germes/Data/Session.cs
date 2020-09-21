using Germes.Data.Models;
using Germes.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data
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
