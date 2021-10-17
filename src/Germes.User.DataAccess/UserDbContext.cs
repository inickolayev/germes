using Germes.User.DataAccess.Maps;
using Microsoft.EntityFrameworkCore;

namespace Germes.User.DataAccess
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options){}

        public DbSet<Domain.Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Build<Domain.Models.User>();
        }
    }
}