using System;
using System.Threading;
using System.Threading.Tasks;
using Germes.User.Contracts.Inbound;
using Germes.User.Domain.Repositories;
using Germes.User.Domain.Services;
using Germes.User.Domain.UnitOfWork;

namespace Germes.User.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserUnitOfWork _userUnitOfWork;

        public UserService(IUserReadRepository userReadRepository, IUserUnitOfWork userUnitOfWork)
        {
            _userReadRepository = userReadRepository;
            _userUnitOfWork = userUnitOfWork;
        }

        public async Task<Domain.Models.User> GetUserAsync(Guid userId, CancellationToken token)
            => await _userReadRepository.GetUserAsync(userId, token);

        public async Task<Domain.Models.User> GetUserAsync(string chatId, CancellationToken token)
            => await _userReadRepository.GetUserAsync(chatId, token);

        public async Task<Domain.Models.User> AddUserAsync(AddUserRequest userRequest, CancellationToken token)
        {
            var user = new Domain.Models.User(userRequest);
            _userUnitOfWork.Users.RegisterNew(user);
            await _userUnitOfWork.Complete(token);

            return user;
        }
    }
}