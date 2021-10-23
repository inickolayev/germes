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
