using Germes.Accountant.DataAccess;
using Germes.Accountant.Domain.Repositories;
using Germes.Accountant.Domain.Services;
using Germes.Accountant.Domain.UnitOfWork;
using Germes.Accountant.Implementations.Plugins;
using Germes.Accountant.Implementations.Repositories;
using Germes.Accountant.Implementations.Services;
using Germes.Accountant.Implementations.UnitOfWork;
using Germes.Domain.Plugins;
using Germes.User.DataAccess;
using Germes.User.Domain.Repositories;
using Germes.User.Domain.Services;
using Germes.User.Domain.UnitOfWork;
using Germes.User.Implementations.Repositories;
using Germes.User.Implementations.Services;
using Germes.User.Implementations.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Germes.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext<TDbContext>(this IServiceCollection services,
            string connectionString)
            where TDbContext : DbContext
            => services.AddDbContextPool<TDbContext>(
                options =>
                {
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                        b =>
                        {
                            b.EnableRetryOnFailure(2);
                            b.MigrationsAssembly(nameof(Germes));
                        });
                    // options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));
                });

        public static IServiceCollection AddUserDomain(this IServiceCollection services)
            => services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserReadRepository>(sp =>
                    new UserReadRepository(sp.GetRequiredService<UserDbContext>().Users))
                .AddScoped<IUserUnitOfWork, UserUnitOfWork>();

        public static IServiceCollection AddAccountantDomain(this IServiceCollection services)
            => services
                .AddScoped<IBotPlugin, AccountantPlugin>()
                .AddScoped<IAccountantService, AccountantService>()
                .AddScoped<ICategoryReadRepository>(sp =>
                    new CategoryReadRepository(sp.GetRequiredService<AccountantDbContext>().Categories))
                .AddScoped<ITransactionReadRepository>(sp =>
                    new TransactionReadRepository(
                        sp.GetRequiredService<AccountantDbContext>().Transactions,
                        sp.GetRequiredService<AccountantDbContext>().Categories))
                .AddScoped<IAccountantUnitOfWork, AccountantUnitOfWork>();
    }
}