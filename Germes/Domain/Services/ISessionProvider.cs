using Germes.Domain.Data;
using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;

namespace Germes.Implementations.Services
{
    /// <summary>
    ///     Менеджер сессий
    /// </summary>
    public interface ISessionManager
    {
        /// <summary>
        ///     Текущая сессия
        /// </summary>
        public Session CurrentSession { get; }

        /// <summary>
        ///     Выставить текущую сессию
        /// </summary>
        /// <param name="session">Сессия</param>
        OperationResult SetCurrentSession(Session session);
    }
}
