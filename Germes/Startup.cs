using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Germes.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using MediatR;
using Germes.Services;
using Microsoft.OpenApi.Models;
using Ngrok.Adapter.Service;

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
            _botSettings = configuration.GetSection("Bot").Get<BotSettings>();
            _appSettings = configuration.GetSection("ApplicationInfo").Get<ApplicationInfoSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Infrastructure
            services.AddControllers().AddNewtonsoftJson();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Germes API", Version = "v1" });
            });

            // Settings
            services.AddSingleton(_botSettings);

            // Services
            services.AddHostedService<InitService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<AccountantService>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<INgrokService>(s => new NgrokService(_botSettings.NgrokHost));
            services.AddScoped(serv => new TelegramBotClient(_botSettings.Token));
            services.AddScoped<IApplicationInfoService, ApplicationInfoService>();
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
