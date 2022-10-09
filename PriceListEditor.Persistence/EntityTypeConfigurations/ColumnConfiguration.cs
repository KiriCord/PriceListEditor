using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriceListEditor.Domain;

namespace PriceListEditor.Persistence.EntityTypeConfigurations;

public class ColumnConfiguration : IEntityTypeConfiguration<Column>
{
    public void Configure(EntityTypeBuilder<Column> builder)
    {
        builder.HasKey(column => column.Id);
        builder.HasIndex(column => column.Id).IsUnique();
        builder.HasMany(u => u.Products)
            .WithMany(c => c.Columns)
            .UsingEntity(j => j.ToTable("ProductsAndColumns"));
        builder.Property(column => column.Name).HasMaxLength(250);
        builder.Property(column => column.Value);
        builder.Property(column => column.Type).HasConversion(v => v.ToString(),
            v => (TypeString)Enum.Parse(typeof(TypeString), v));
    }
}