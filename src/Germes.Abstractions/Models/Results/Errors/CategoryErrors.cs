namespace Germes.Abstractions.Models.Results.Errors
{
    public static class CategoryErrors
    {
        public static BusinessError CategoryNotExist(string category) => new BusinessError($"Категории \"{category}\" не существует");
    }
}
