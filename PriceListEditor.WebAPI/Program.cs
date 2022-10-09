using PriceListEditor.Application;
using PriceListEditor.Application.Common.Mappings;
using PriceListEditor.Application.Interfaces;
using PriceListEditor.Persistence;
using PriceListEditor.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(typeof(Program).Assembly));
    config.AddProfile(new AssemblyMappingProfile(typeof(IPriceListEditorDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllersWithViews();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<PriceListEditorDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        Console.WriteLine(exception);
        throw new Exception();
    }
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();