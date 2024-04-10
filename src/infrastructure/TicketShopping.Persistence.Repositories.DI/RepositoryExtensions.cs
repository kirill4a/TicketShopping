using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketShopping.Domain.Aggregates.Airport;
using TicketShopping.Persistence.Repositories.Airport;

namespace TicketShopping.Persistence.Repositories.DI;

public static class RepositoryExtensions
{
    /// <summary>
    /// Custom Repository configuration. See <see cref="RepositoryExtensions"/> extension class
    /// </summary>
    public static IServiceCollection RegisterRepositories(this IServiceCollection services,
                                                          string connectionString)
    {
        RegisterDbContext(services, connectionString);

        services.AddScoped<IAirportRepository>(sp =>
        {
            var dbContext = sp.GetRequiredService<TicketShoppingDbContext>();
            return new AirportRepository(dbContext.Airports);
        });

        return services;
    }

    private static void RegisterDbContext(IServiceCollection services, string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

        services.AddDbContext<TicketShoppingDbContext>(options =>
                                                options.UseMySql(connectionString,
                                                ServerVersion.AutoDetect(connectionString)));
    }
}
