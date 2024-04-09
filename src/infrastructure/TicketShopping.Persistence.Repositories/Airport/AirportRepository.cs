using LinqKit;
using Microsoft.EntityFrameworkCore;
using TicketShopping.Domain.Aggregates.Airport;
using DomainAirport = TicketShopping.Domain.Aggregates.Airport.Airport;

namespace TicketShopping.Persistence.Repositories.Airport;

internal class AirportRepository : IAirportRepository
{
    public async Task<IEnumerable<DomainAirport>> FilterAsync(AirportSearchFilter filter,
                                                              CancellationToken cancellation)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));

        if (filter.IsEmpty)
            return [];

        //TODO: remove the stub with implementation
        var stubData = Enumerable.Range(1, 1)
                                 .Select(_ => new DomainAirport(new(1),
                                                                new(51.470020, -0.454295),
                                                                new("Heathrow Airport London"),
                                                                new("LHR"),
                                                                new("EGLL")));

        var predicate = PredicateBuilder.New<DomainAirport>();

        if (!string.IsNullOrWhiteSpace(filter.AirportName))
        {
            predicate = predicate.And(a => a.Name.Name.Contains(filter.AirportName));
        }

        if (!string.IsNullOrWhiteSpace(filter.AirportIataCode))
        {
            predicate = predicate.Or(a => a.IataCode != null && a.IataCode.Code.Contains(filter.AirportIataCode));
        }

        if (!string.IsNullOrWhiteSpace(filter.AirportIcaoCode))
        {
            predicate = predicate.Or(a => a.IcaoCode != null && a.IcaoCode.Code.Contains(filter.AirportIcaoCode));
        }

        var query = stubData.Where(predicate);

        var result = query.ToList();

        return await Task.FromResult(result).ConfigureAwait(false);
    }
}