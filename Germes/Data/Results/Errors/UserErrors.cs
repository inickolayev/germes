using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Results.Errors
{
    public static class UserErrors
    {
        public static BusinessError UserNotExist(string chatId) => new BusinessError($"Пользователя с chatId=\"{chatId}\" не существует");
    }
}
