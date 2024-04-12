using TicketShopping.Integration.Aviationstack.Models.Airport;

namespace TicketShopping.Integration.Aviationstack.Client;

public interface IAviationstackClient
{
    Task<IEnumerable<Airport>> GetAirports(CancellationToken cancellation);
}
