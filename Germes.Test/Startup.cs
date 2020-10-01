using Germes.Data;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Pipelines;
using Germes.Services;
using Germes.Test.Mocks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Germes.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Mediator
            services.AddMediatR(c => c.AsScoped(), Assembly.GetAssembly(typeof(Germes.Startup)));
            services.AddScoped(typeof(IPipelineBehavior<RequestNewMessage, OperationResult<BotResult>>), typeof(SessionAddBehavior));

            // Services
            services.AddScoped<IApplicationInfoService, ApplicationInfoService>();
            services.AddScoped<IBotService, BotService>();
            services.AddScoped<ISessionManager, SessionManager>();

            // Mocks
            services.AddScoped<IUserService, UserServiceMock>();
            services.AddScoped<ISessionService, SessionServiceMock>();
            services.AddScoped<IAccountantService, AccountantServiceMock>();
            services.AddScoped<ICategoryService, CategoryServiceMock>();
            services.AddScoped<IPluginService, PluginServiceMock>();
        }
    }
}
