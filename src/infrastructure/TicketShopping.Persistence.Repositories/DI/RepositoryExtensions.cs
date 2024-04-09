using Microsoft.Extensions.DependencyInjection;
using TicketShopping.Domain.Aggregates.Airport;
using TicketShopping.Persistence.Repositories.Airport;

namespace TicketShopping.Persistence.Repositories.DI;

public static class RepositoryExtensions
{
    /// <summary>
    /// Custom Repository configuration. See <see cref="RepositoryExtensions"/> extension class
    /// </summary>
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAirportRepository>(_ => new AirportRepository());
        
        return services;
    }
}
