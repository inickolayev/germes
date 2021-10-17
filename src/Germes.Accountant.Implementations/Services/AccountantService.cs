using System;
using System.Threading;
using System.Threading.Tasks;
using Germes.Abstractions.Extensions;
using Germes.Accountant.Contracts.Dto;
using Germes.Accountant.Domain.Models;
using Germes.Accountant.Domain.Repositories;
using Germes.Accountant.Domain.Services;
using Germes.Accountant.Domain.UnitOfWork;
using Inbound = Germes.Accountant.Contracts.Inbound;
using Dto = Germes.Accountant.Contracts.Dto;

namespace Germes.Accountant.Implementations.Services
{
    public class AccountantService : IAccountantService
    {
        private readonly IAccountantUnitOfWork _unitOfWork;
        private readonly ITransactionReadRepository _transactionReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;

        private static readonly DateTime DefaultDateFrom = DateTime.Now.GetFirstDateOfMonth();
        private static readonly DateTime DefaultDateTo = DateTime.Now.GetFirstDateOfMonth();
        
        public AccountantService(
            IAccountantUnitOfWork unitOfWork,
            ITransactionReadRepository transactionReadRepository,
            ICategoryReadRepository categoryReadRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionReadRepository = transactionReadRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task AddTransaction(Inbound.AddTransactionRequest request, CancellationToken cancellationToken)
        {
            _unitOfWork.Transactions.RegisterNew(new Transaction(request));
            await _unitOfWork.Complete(cancellationToken);
        }

        public async Task<decimal> GetBalance(Guid userId, CancellationToken cancellationToken)
            => await _transactionReadRepository.GetBalance(userId, cancellationToken);
        
        public async Task<decimal> GetBalance(Guid userId, string categoryName, CancellationToken cancellationToken)
            => await GetBalance(userId, categoryName, DefaultDateFrom, DefaultDateTo, cancellationToken);
        
        public async Task<decimal> GetBalance(Guid userId,
            string categoryName,
            DateTime @from,
            DateTime to,
            CancellationToken cancellationToken)
        {
            var category = await _categoryReadRepository.GetCategory(userId, categoryName, cancellationToken);
            if (category != null)
            {
                return await _transactionReadRepository.GetBalance(userId, category.Id, DefaultDateFrom, DefaultDateTo, cancellationToken);
            }

            return 0.0m;
        }
        
        public async Task<decimal> GetBalance(Guid userId, Guid categoryId, CancellationToken cancellationToken)
            => await GetBalance(userId, categoryId, DefaultDateFrom, DefaultDateTo, cancellationToken);
        
        public async Task<decimal> GetBalance(Guid userId,
            Guid categoryId,
            DateTime @from,
            DateTime to,
            CancellationToken cancellationToken)
            => await _transactionReadRepository.GetBalance(userId, categoryId, DefaultDateFrom, DefaultDateTo, cancellationToken);

        public async Task<Dto.ExpenseCategoryDto> GetExpenseCategory(Guid userId, string categoryName, CancellationToken token)
            => (await _categoryReadRepository.GetExpenseCategory(userId, categoryName, token)).ToExpenseCategoryDto();

        public async Task<Dto.IncomeCategoryDto> GetIncomeCategory(Guid userId, string categoryName, CancellationToken token)
            => (await _categoryReadRepository.GetIncomeCategory(userId, categoryName, token)).ToIncomeCategoryDto();

        public async Task<ExpenseCategoryDto> AddCategory(Inbound.AddExpenseCategoryRequest request, CancellationToken token)
        {
            var newCategory = new Category(request);
            _unitOfWork.Categories.RegisterNew(newCategory);
            await _unitOfWork.Complete(token);

            return newCategory.ToExpenseCategoryDto();
        }

        public async Task<IncomeCategoryDto> AddCategory(Inbound.AddIncomeCategoryRequest request, CancellationToken token)
        {
            var newCategory = new Category(request);
            _unitOfWork.Categories.RegisterNew(newCategory);
            await _unitOfWork.Complete(token);

            return newCategory.ToIncomeCategoryDto();
        }
    }
}