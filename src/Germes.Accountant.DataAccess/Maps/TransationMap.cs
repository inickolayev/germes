using Germes.Accountant.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Germes.Accountant.DataAccess.Maps
{
    public static class ExpenseMap
    {
        public static ModelBuilder Build<T>(this ModelBuilder modelBuilder) where T: Transaction
        {
            var entityBuilder = modelBuilder.Entity<T>();

            // Main
            entityBuilder.ToTable("transactions");

            // Indexes
            entityBuilder.HasIndex(_ => _.Id);
            entityBuilder.HasIndex(_ => _.UserId);
            entityBuilder.HasIndex(_ => _.CategoryId);
            entityBuilder.HasIndex(_ => _.CreatedAt);

            // Values
            entityBuilder.Property(_ => _.Id)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<GuidValueGenerator>();

            entityBuilder.Property(_ => _.RowVersion)
                .IsRowVersion();

            return modelBuilder;
        }
    }
}
