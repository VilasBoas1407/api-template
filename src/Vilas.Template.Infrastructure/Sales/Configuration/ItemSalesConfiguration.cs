using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vilas.Template.Domain.Sales;

namespace Vilas.Template.Infrastructure.Sales.Configuration
{
    public class ItemSalesConfiguration : IEntityTypeConfiguration<ItemSale>
    {
        public void Configure(EntityTypeBuilder<ItemSale> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasMaxLength(50);

            builder.Property(x => x.Price);

            builder.Property(x => x.Quantity);
        }
    }
}
