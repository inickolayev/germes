using Germes.Abstractions.UnitOfWork;
using Germes.User.Domain.Repositories;

namespace Germes.User.Domain.UnitOfWork
{
    public interface IUserUnitOfWork : IUnitOfWork
    {
        IUserRegisterRepository Users { get; }
    }
}
