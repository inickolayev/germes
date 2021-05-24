using Germes.Data;
using Germes.Data.Models;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Extensions;
using Germes.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Germes.Test.Plugins
{
    public abstract class AbstractPluginTests : IAsyncLifetime
    {
        protected readonly IUserService _userService;
        protected readonly ISessionService _sessionService;
        protected readonly IServiceProvider _serviceProvider;

        protected AbstractPluginTests(
            IUserService userService,
            ISessionService sessionService,
            IServiceProvider serviceProvider
        ) {
            _userService = userService;
            _sessionService = sessionService;
            _serviceProvider = serviceProvider;
        }

        public async Task InitializeAsync()
        {
            await AddDefaultUserAsync();
            await AddDefaultSessionAsync();
        }

        public async Task DisposeAsync()
        {
        }

        /// <summary>
        ///     Отправить запрос через медиатор
        /// </summary>
        protected Task<OperationResult<TResult>> MSendSafe<TRequest, TResult>(TRequest req, CancellationToken token = default)
            where TRequest : IRequest<OperationResult<TResult>>
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            return mediator.SendSafe<TRequest, TResult>(req);
        }

        /// <summary>
        ///     Отправить запрос через медиатор
        /// </summary>
        protected Task<OperationResult> MSendSafe<TRequest>(TRequest req, CancellationToken token = default)
            where TRequest : IRequest<OperationResult>
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            return mediator.SendSafe(req);
        }

        /// <summary>
        ///     Создать сообщение
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        /// <param name="chatId">Id чата</param>
        protected RequestNewMessage CreateNewMessage(string text, string chatId = TestConsts.ChatIdDefault)
           => new RequestNewMessage
           {
               Message = new BotMessage
               {
                   ChatId = chatId,
                   Text = text
               }
           };

        protected async Task<ExpenseModel> AddExpenseAsync(decimal cost, string category, CancellationToken token = default)
        {
            using var scope = _serviceProvider.CreateScope();
            var accountant = scope.ServiceProvider.GetService<IAccountantService>();
            var categoryService = scope.ServiceProvider.GetService<ICategoryService>();
            var expenseRes = await accountant.AddAsync(new ExpenseModel
            {
                Category = (await categoryService.AddExpenseCategoryAsync(category, "", token)).Result,
                Cost = cost,
                Date = DateTime.Now,
                SubCategory = null
            }, token);
            return expenseRes.Result;
        }

        protected async Task<IncomeModel> AddIncomeAsync(decimal cost, string category, CancellationToken token = default)
        {
            using var scope = _serviceProvider.CreateScope();
            var accountant = scope.ServiceProvider.GetService<IAccountantService>();
            var categoryService = scope.ServiceProvider.GetService<ICategoryService>();
            var incomeRes = await accountant.AddAsync(new IncomeModel
            {
                Category = (await categoryService.AddIncomeCategoryAsync(category, "", token)).Result,
                Cost = cost,
                Date = DateTime.Now
            }, token);
            return incomeRes.Result;
        }

        protected async Task<UserModel> AddDefaultUserAsync(CancellationToken token = default)
        {
            var user = new UserModel
            {
                Name = TestConsts.UserNameDefault
            };
            await _userService.AddUserAsync(user);
            return user;
        }

        protected async Task<SessionModel> AddDefaultSessionAsync(CancellationToken token = default)
        {
            var session = new SessionModel
            {
                ChatId = TestConsts.ChatIdDefault,
                User = await AddDefaultUserAsync(token)
            };
            await _sessionService.AddSessionAsync(session);
            return session;
        }
    }
}
