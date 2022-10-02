namespace PriceListEditor.Persistence;

public class DbInitializer
{
    public static void Initialize(PriceListEditorDbContext context)
    {
        context.Database.EnsureCreated();
    }
}