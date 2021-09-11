namespace Germes.Abstractions.Models.Results.Errors
{
    public static class UserErrors
    {
        public static BusinessError UserNotExist(string chatId) => new BusinessError($"Пользователя с chatId=\"{chatId}\" не существует");
    }
}
