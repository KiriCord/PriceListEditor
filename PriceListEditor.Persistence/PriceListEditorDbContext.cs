using Microsoft.EntityFrameworkCore;
using PriceListEditor.Application.Interfaces;
using PriceListEditor.Domain;
using PriceListEditor.Persistence.EntityTypeConfigurations;

namespace PriceListEditor.Persistence;

public class PriceListEditorDbContext : DbContext, IPriceListEditorDbContext
{
    public DbSet<PriceList> PriceLists { get; set; }
    public DbSet<Product> Products { get; set; }

    public PriceListEditorDbContext(DbContextOptions<PriceListEditorDbContext> options)
        : base(options){ }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new PriceListConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        base.OnModelCreating(builder);
    }
}