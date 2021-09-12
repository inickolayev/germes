using System.Reflection;
using Germes.Abstractions.Repositories;
using Germes.Abstractions.Services;
using Germes.Accountant.Domain.Repositories;
using Germes.Accountant.Implementations.Repositories;
using Germes.Configurations;
using Germes.Domain.Data;
using Germes.Implementations.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using MediatR;
using Germes.Implementations.Services;
using Microsoft.OpenApi.Models;
using Ngrok.Adapter.Service;
using Germes.Mediators.Pipelines;

namespace Germes
{
    public class Startup
    {
        private readonly BotSettings _botSettings;
        private readonly ApplicationInfoSettings _appSettings;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _botSettings = configuration.GetSection("TelegramBot").Get<BotSettings>();
            _appSettings = configuration.GetSection("ApplicationInfo").Get<ApplicationInfoSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Infrastructure
            services.AddControllers().AddNewtonsoftJson();
            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Germes API", Version = "v1" });
            });
            // Mediator
            services.AddMediatR(c => c.AsScoped(), Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // Settings
            services.AddSingleton(_botSettings);

            // Services
            // services.AddHostedService<InitService>();
            services.AddSingleton<INgrokService>(s => new NgrokService(_botSettings.NgrokHost));
            services.AddScoped(serv => new TelegramBotClient(_botSettings.Token));
            services.AddScoped<IBotService, BotService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IApplicationInfoService, ApplicationInfoService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountantReadRepository, AccountantReadRepository>();
            services.AddScoped<IAccountantRegisterRepository, AccountantRegisterRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryRegisterRepository, CategoryRegisterRepository>();
            services.AddScoped<IPluginService, PluginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Germes API V1");
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
