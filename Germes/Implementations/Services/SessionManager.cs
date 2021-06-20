using Germes.Domain.Data;
using Germes.Domain.Data.Models;
using Germes.Domain.Data.Results;
using Germes.Domain.Data.Results.Errors;

namespace Germes.Implementations.Services
{
    public class SessionManager : ISessionManager
    {
        public Session CurrentSession { get; private set; }

        public OperationResult SetCurrentSession(Session session)
        {
            if (CurrentSession != null)
                return new OperationResult(SessionErrors.CurrentSessionAlreadySet());
            CurrentSession = session;
            return new OperationResult();
        }
    }
}
