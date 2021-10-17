using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results.Errors;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;
using Germes.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Germes.Accountant.Implementations.Repositories
{
    /// <summary>
    ///     Сервис хранения категорий
    /// </summary>
    public class CategoryRegisterRepository : RegisterRepository<Category>, ICategoryRegisterRepository
    {
        public CategoryRegisterRepository(DbSet<Category> dbSet) : base(dbSet)
        {
        }
    }
}
