namespace Germes.Abstractions.Models.Results.Errors
{
    public static class SessionErrors
    {
        public static BusinessError SessionNotExist(string chatId) => new BusinessError($"Сессии с chatId=\"{chatId}\" не существует");
        public static BusinessError CurrentSessionAlreadySet() => new BusinessError($"Текущая сессия уже выставлена");
    }
}
