using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Models.Results.Errors;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;
using Germes.Implementations.Repositories;
using Germes.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using DomainModels = Germes.User.Domain.Models;

namespace Germes.User.Implementations.Repositories
{
    public class UserRegisterRepository : RegisterRepository<DomainModels.User>, IUserRegisterRepository
    {
        public UserRegisterRepository(DbSet<DomainModels.User> dbSet) : base(dbSet)
        {
        }
    }
}
