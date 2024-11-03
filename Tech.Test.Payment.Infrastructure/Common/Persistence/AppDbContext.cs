using Microsoft.EntityFrameworkCore;
using Tech.Test.Payment.Domain.Sales;

namespace Tech.Test.Payment.Infrastructure.Common.Persistence
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Sale> Sale { get; set; }
        public DbSet<ItemSale> ItemSale { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
