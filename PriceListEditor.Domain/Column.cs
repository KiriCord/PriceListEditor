namespace PriceListEditor.Domain;

public enum TypeString
{
    text,
    number,
    textarea
}
public class Column
{
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public string Name { get; set; }
    public string? Value { get; set; }
    public TypeString Type { get; set; }
}