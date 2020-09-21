using Germes.Data;
using Germes.Data.Results;
using Germes.Data.Results.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Services
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
