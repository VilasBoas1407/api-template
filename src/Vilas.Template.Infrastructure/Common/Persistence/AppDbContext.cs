using Microsoft.EntityFrameworkCore;
using Vilas.Template.Domain.Sales;

namespace Vilas.Template.Infrastructure.Common.Persistence;

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
