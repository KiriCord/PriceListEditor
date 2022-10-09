namespace PriceListEditor.Domain;

public class PriceList
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; } = new();
}