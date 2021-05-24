//using Germes.Data.Models;
//using Germes.Data.Results;
//using Germes.Data.Results.Errors;
//using Germes.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Telegram.Bot.Types;

//namespace Germes.Test.Mocks
//{
//    public class UserServiceMock : IUserService, IDisposable
//    {
//        public static readonly List<UserModel> Users = new List<UserModel>();

//        public async Task<OperationResult<UserModel>> GetUserAsync(string userId, CancellationToken token)
//            => new OperationResult<UserModel>(Users.SingleOrDefault(us => us.Id == userId));

//        public async Task<OperationResult<UserModel>> AddUserAsync(UserModel user, CancellationToken token)
//        {
//            if (Users.Any(us => us.ChatId == user.ChatId))
//                return new OperationResult<UserModel>(UserErrors.UserNotExist(user.ChatId));
//            Users.Add(user);
//            return new OperationResult<UserModel>(user);
//        }

//        public void Dispose()
//        {
//            Users.Clear();
//        }
//    }
//}
