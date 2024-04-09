
namespace TicketShopping.Domain.Aggregates.Airport;

public record AirportIata
{
    public AirportIata(string iataAirportCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(iataAirportCode, nameof(iataAirportCode));
        if (iataAirportCode.Length != 3)
            throw new ArgumentException($"IATA airport code '{iataAirportCode}' is not valid", nameof(iataAirportCode));

        Code = iataAirportCode;
    }

    public string Code { get; }
}
