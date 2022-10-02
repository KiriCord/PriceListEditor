namespace PriceListEditor.Domain;

public class Product
{
    public Guid Id { get; set; }
    public Guid IdPriceList { get; set; }
    public string? ProductName { get; set; }
    public int ProductCode { get; set; }
}