using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tech.Test.Payment.Domain.Sales;

namespace Tech.Test.Payment.Infrastructure.Sales.Configuration
{
    public class SalesConfigurations : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever();

            builder.Property(u => u.CustomerName)
                .HasMaxLength(1000);

            builder.Property(u => u.CustomerPhone)
                .HasMaxLength(30);

            builder.HasKey(u => u.SellerId);

            builder.Property(u => u.SellerName)
                .HasMaxLength(1000);

            builder.Property(u => u.SellerCpf)
                .HasMaxLength(11);

            builder.Property(u => u.SellerEmail)
                .HasMaxLength(150);

            builder.Property(u => u.SellerPhone)
                .HasMaxLength(30);

            builder.Property(u => u.Status);

            builder.Property(u => u.SaleDate);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Sale)
                .HasForeignKey(x => x.SaleId);
        }
    }
}
