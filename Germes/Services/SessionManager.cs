using Germes.Data;
using Germes.Data.Models;
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
        public SessionModel CurrentSession { get; private set; }

        public OperationResult SetCurrentSession(SessionModel session)
        {
            if (CurrentSession != null)
                return new OperationResult(SessionErrors.CurrentSessionAlreadySet());
            CurrentSession = session;
            return new OperationResult();
        }
    }
}
