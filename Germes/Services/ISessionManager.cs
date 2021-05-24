﻿using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Results;
using Germes.Data.Results.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Services
{
    /// <summary>
    ///     Менеджер сессий
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        ///     Текущая сессия
        /// </summary>
        public SessionModel CurrentSession { get; }

        /// <summary>
        ///     Выставить текущую сессию
        /// </summary>
        /// <param name="session">Сессия</param>
        OperationResult SetCurrentSession(SessionModel session);
    }
}
