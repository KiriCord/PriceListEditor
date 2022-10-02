namespace PriceListEditor.Domain;

public class PriceList
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Product> Products;

    public PriceList() =>
        Products = new List<Product>();
}