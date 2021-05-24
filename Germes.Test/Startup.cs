using Germes.Data;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Pipelines;
using Germes.Services;
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
            services.AddSingleton<IUserService, UserService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IAccountantService, AccountantService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPluginService, PluginService>();
        }
    }
}
