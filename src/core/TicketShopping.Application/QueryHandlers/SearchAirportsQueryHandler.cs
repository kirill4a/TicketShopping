using MediatR;
using TicketShopping.Application.Contracts.Dto;
using TicketShopping.Application.Contracts.Queries;
using TicketShopping.Domain.Aggregates.Airport;

namespace TicketShopping.Application.QueryHandlers;

internal class SearchAirportsQueryHandler : IRequestHandler<SearchAirportsQuery, IEnumerable<AirportDto>>
{
    private readonly IAirportRepository _airportRepository;

    public SearchAirportsQueryHandler(IAirportRepository airportRepository)
    {
        _airportRepository = airportRepository ?? throw new ArgumentNullException(nameof(airportRepository));
    }

    public async Task<IEnumerable<AirportDto>> Handle(SearchAirportsQuery query, CancellationToken cancellation)
    {
        ArgumentNullException.ThrowIfNull(query);

        var filter = BuildFilter(query);
        var domainAirports = await _airportRepository.FilterAsync(filter, cancellation).ConfigureAwait(false);
        if (domainAirports == null || !domainAirports.Any())
            return [];

        return domainAirports.Select(Map);
    }

    private static AirportSearchFilter BuildFilter(SearchAirportsQuery query)
    {
        query.Deconstruct(out var searchQuery);

        return new AirportSearchFilter()
                    .WithAirportName(searchQuery!)
                    .WithAirportIataCode(searchQuery!)
                    .WithAirportIcaoCode(searchQuery!);
    }

    //TODO: consider using a separate mapping layer, factory, mapper, or whatever....
    private static AirportDto Map(Airport domainAirport)
    {
        ArgumentNullException.ThrowIfNull(domainAirport);

        return new()
        {
            Id = domainAirport.Id.Id,
            Name = domainAirport.Name.Name,
            IataCode = domainAirport.IataCode?.Code,
            IcaoCode = domainAirport.IcaoCode?.Code,
            Location = new()
            {
                Latitude = domainAirport.Location.Latitude,
                Longitude = domainAirport.Location.Longitude
            }
        };
    }
}
