using TicketShopping.Domain.ValueObjects;

namespace TicketShopping.Domain.Aggregates.Airport;

public class Airport : EntityBase<AirportId>
{
    public Airport(
                AirportId Id,
                GeoLocation location,
                AirportName name,
                AirportIata? iataCode,
                AirportIcao? icaoCode)
        : base(Id)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        Location = location;
        Name = name;
        IataCode = iataCode;
        IcaoCode = icaoCode;
    }

    public GeoLocation Location { get; }
    public AirportName Name { get; }
    public AirportIata? IataCode { get; }
    public AirportIcao? IcaoCode { get; }
}
