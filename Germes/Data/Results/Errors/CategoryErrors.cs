using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Germes.Data.Results.Errors
{
    public static class CategoryErrors
    {
        public static BusinessError CategoryNotExist(string category) => new BusinessError($"Категории \"{category}\" не существует");
    }
}
