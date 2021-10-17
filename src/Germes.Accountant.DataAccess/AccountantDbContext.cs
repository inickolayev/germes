using Germes.Accountant.DataAccess.Maps;
using Germes.Accountant.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Germes.Accountant.DataAccess
{
    public class AccountantDbContext : DbContext
    {
        public AccountantDbContext(DbContextOptions<AccountantDbContext> options)
            : base(options){}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Build<Category>();
            builder.Build<Transaction>();
        }
    }
}