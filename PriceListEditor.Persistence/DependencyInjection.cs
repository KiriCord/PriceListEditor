using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceListEditor.Application.Interfaces;

namespace PriceListEditor.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["DbConnection"];
        services.AddDbContext<PriceListEditorDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IPriceListEditorDbContext>(provider =>
            provider.GetService<PriceListEditorDbContext>());
        return services;
    }
}