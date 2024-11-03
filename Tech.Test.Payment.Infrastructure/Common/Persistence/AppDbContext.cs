using Microsoft.EntityFrameworkCore;
using Tech.Test.Payment.Domain.Sales;

namespace Tech.Test.Payment.Infrastructure.Common.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Sale> Sales { get; set; } 
    public DbSet<ItemSale> ItemSales { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
