using Germes.Abstractions.Repositories;
using Germes.Accountant.Domain.Models;

namespace Germes.Accountant.Domain.Repositories
{
    /// <summary>
    ///     Сервис подсчета расходов/доходов
    /// </summary>
    public interface ITransactionRegisterRepository : IRegisterRepository<Transaction>
    {
    }
}
