namespace TicketShopping.Domain.Aggregates.Airport;

public interface IAirportRepository
{
    Task<IEnumerable<Airport>> FilterAsync(AirportSearchFilter filter, CancellationToken cancellation);
}
