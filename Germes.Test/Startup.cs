using Germes.Data;
using Germes.Data.Requests;
using Germes.Data.Results;
using Germes.Pipelines;
using Germes.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IBotService, BotService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionManager, SessionManager>();
            services.AddScoped<IApplicationInfoService, ApplicationInfoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountantService, AccountantService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPluginService, PluginService>();
        }
    }
}
