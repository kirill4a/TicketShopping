using LinqKit;
using Microsoft.EntityFrameworkCore;
using TicketShopping.Domain.Aggregates.Airport;
using TicketShopping.Domain.ValueObjects;
using DbAirport = TicketShopping.Persistence.Entities.Models.Airport;
using DomainAirport = TicketShopping.Domain.Aggregates.Airport.Airport;

namespace TicketShopping.Persistence.Repositories.Airport;

internal class AirportRepository : RepositoryBase<DbAirport>, IAirportRepository
{
    public AirportRepository(DbSet<DbAirport> set) : base(set)
    {
    }

    public void AddRange(IEnumerable<DomainAirport> airports)
    {
        ArgumentNullException.ThrowIfNull(airports, nameof(airports));
        Set.AddRange(airports.Select(MapToDb).ToList());
    }

    public async Task<IEnumerable<DomainAirport>> FilterAsync(AirportSearchFilter filter,
                                                              CancellationToken cancellation)
    {
        ArgumentNullException.ThrowIfNull(filter, nameof(filter));

        if (filter.IsEmpty)
            return [];

        var predicate = PredicateBuilder.New<DbAirport>();

        if (!string.IsNullOrWhiteSpace(filter.AirportName))
        {
            predicate = predicate.And(a => a.Name.Contains(filter.AirportName));
        }

        if (!string.IsNullOrWhiteSpace(filter.AirportIataCode))
        {
            predicate = predicate.Or(a => a.Iata != null && a.Iata.Contains(filter.AirportIataCode));
        }

        if (!string.IsNullOrWhiteSpace(filter.AirportIcaoCode))
        {
            predicate = predicate.Or(a => a.Icao != null && a.Icao.Contains(filter.AirportIcaoCode));
        }

        var query = Set.Where(predicate);

        var dbEntities = await query.ToListAsync(cancellation).ConfigureAwait(false);

        return dbEntities.Select(MapToDomain);
    }

    //TODO: consider using a separate mapping layer, factory, mapper, or whatever....
    private static DomainAirport MapToDomain(DbAirport dbAirport)
    {
        ArgumentNullException.ThrowIfNull(dbAirport);

        return new DomainAirport(new AirportId(dbAirport.Id),
                                 new GeoLocation(dbAirport.Latitude, dbAirport.Longitude),
                                 new AirportName(dbAirport.Name),
                                 dbAirport.Iata is null ? null : new AirportIata(dbAirport.Iata),
                                 dbAirport.Icao is null ? null : new AirportIcao(dbAirport.Icao));
    }

    private static DbAirport MapToDb(DomainAirport domainAirport)
    {
        ArgumentNullException.ThrowIfNull(domainAirport);

        return new DbAirport
        {
            Id = domainAirport.Id.Id,
            Name = domainAirport.Name.Name,
            Iata = domainAirport.IataCode?.Code,
            Icao = domainAirport.IcaoCode?.Code,
            Latitude = domainAirport.Location.Latitude,
            Longitude = domainAirport.Location.Longitude
        };
    }
}