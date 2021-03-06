﻿using Germes.Data.Models;
using Germes.Data.Results;
using Germes.Data.Results.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Germes.Services
{
    public class UserService : IUserService
    {
        private static readonly List<UserModel> _users = new List<UserModel>();

        public async Task<OperationResult<UserModel>> GetUserAsync(string chatId, CancellationToken token)
            => new OperationResult<UserModel>(_users.SingleOrDefault(us => us.ChatId == chatId));

        public async Task<OperationResult<UserModel>> AddUserAsync(UserModel user, CancellationToken token)
        {
            if (_users.Any(us => us.ChatId == user.ChatId))
                return new OperationResult<UserModel>(UserErrors.UserNotExist(user.ChatId));
            _users.Add(user);
            return new OperationResult<UserModel>(user);
        }
    }
}
