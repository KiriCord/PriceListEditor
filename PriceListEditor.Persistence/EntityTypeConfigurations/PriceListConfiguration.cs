using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceListEditor.Domain;

namespace PriceListEditor.Persistence.EntityTypeConfigurations;

public class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
{
    public void Configure(EntityTypeBuilder<PriceList> builder)
    {
        builder.HasKey(priceList => priceList.Id);
        builder.HasIndex(priceList => priceList.Id).IsUnique();
        builder.Property(priceList => priceList.Name).HasMaxLength(250);
    }
}