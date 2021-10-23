using System.Reflection;
using Germes.Abstractions.Models;
using Germes.Abstractions.Services;
using Germes.Accountant.DataAccess;
using Germes.Configurations;
using Germes.Domain.Data;
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
using Germes.Telegram.Infrastructure.Services;
using Germes.User.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Germes.Extensions;

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
            var connectionString = _configuration.GetConnectionString("MySqlConnection");
            services
                .AddDbContext<AccountantDbContext>(connectionString)
                .AddDbContext<UserDbContext>(connectionString);
            
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
            services.AddHostedService<InitService>();
            services.AddSingleton<INgrokService>(s => new NgrokService(_botSettings.NgrokHost));
            services.AddScoped(serv => new TelegramBotClient(_botSettings.Token));
            services.AddScoped<IBotService, BotService>();
            services.AddScoped<IApplicationInfoService>(sp => new ApplicationInfoService(
                sp.GetService<ILogger<ApplicationInfoService>>(),
                new ApplicationInfo()
                {
                    Name = _appSettings.Name,
                    Version = _appSettings.Version
                }));
            services.AddScoped<IPluginService, PluginService>();
            services.AddAccountantDomain();
            services.AddUserDomain();
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
            
            UpdateDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            UpdateDatabase<AccountantDbContext>(app);
            UpdateDatabase<UserDbContext>(app);
        }
        
        private void UpdateDatabase<TDbContext>(IApplicationBuilder app)
            where TDbContext : DbContext
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<TDbContext>();
            context?.Database.Migrate();
        }
    }
}
