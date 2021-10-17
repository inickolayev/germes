using Germes.Abstractions.UnitOfWork;
using Germes.Accountant.Domain.Repositories;

namespace Germes.Accountant.Domain.UnitOfWork
{
    public interface IAccountantUnitOfWork : IUnitOfWork
    {
        ICategoryRegisterRepository Categories { get; }
        ITransactionRegisterRepository Transactions { get; }
    }
}
