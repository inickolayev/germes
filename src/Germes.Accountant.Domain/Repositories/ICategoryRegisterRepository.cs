using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Repositories;
using Germes.Accountant.Domain.Models;

namespace Germes.Accountant.Domain.Repositories
{
    public interface ICategoryRegisterRepository : IRegisterRepository<Category>
    {
    }
}
