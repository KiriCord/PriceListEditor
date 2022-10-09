using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceListEditor.Domain;

namespace PriceListEditor.Persistence.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);
        builder.HasIndex(product => product.Id).IsUnique();
        builder.HasOne(u => u.PriceList)
            .WithMany(c => c.Products)
            .HasForeignKey(k => k.PriceListId);
        builder.Property(product => product.ProductCode);
        builder.Property(product => product.ProductName).HasMaxLength(250);
    }
}