using Microsoft.Extensions.DependencyInjection;
using TicketShopping.Application.QueryHandlers;

namespace TicketShopping.Application.DI;

public static class MediatorExtensions
{
    /// <summary>
    /// Custom MediatR configuration. See <see cref="MediatorExtensions"/> extension class
    /// </summary>
    public static IServiceCollection ConfigureMediator(this IServiceCollection services)
       =>
       services.AddMediatR(cfg =>
       {
           cfg.RegisterServicesFromAssemblyContaining<SearchAirportsQueryHandler>();
       });
}
