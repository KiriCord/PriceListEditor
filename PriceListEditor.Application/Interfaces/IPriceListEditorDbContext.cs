using Microsoft.EntityFrameworkCore;
using PriceListEditor.Domain;

namespace PriceListEditor.Application.Interfaces;

public interface IPriceListEditorDbContext
{
    DbSet<PriceList> PriceLists { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Column> Columns { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}