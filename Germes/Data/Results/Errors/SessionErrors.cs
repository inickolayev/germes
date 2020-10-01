using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Results.Errors
{
    public static class SessionErrors
    {
        public static BusinessError SessionNotExist(string chatId) => new BusinessError($"Сессии с chatId=\"{chatId}\" не существует");
        public static BusinessError SessionAlreadyExist(string chatId) => new BusinessError($"Сессия с chatId=\"{chatId}\" уже существует");
        public static BusinessError CurrentSessionAlreadySet() => new BusinessError($"Текущая сессия уже выставлена");
    }
}
