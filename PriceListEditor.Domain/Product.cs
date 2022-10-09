namespace PriceListEditor.Domain;

public class Product
{
    public Guid Id { get; set; }
    public Guid PriceListId { get; set; }
    public PriceList PriceList { get; set; }
    public string ProductName { get; set; }
    public int ProductCode { get; set; }

    public virtual List<Column> Columns { get; set; } = new ();
}