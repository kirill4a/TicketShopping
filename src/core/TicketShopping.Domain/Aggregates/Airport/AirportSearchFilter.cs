namespace TicketShopping.Domain.Aggregates.Airport;

public class AirportSearchFilter
{
    public string? AirportName { get; private set; }
    public string? AirportIataCode { get; private set; }
    public string? AirportIcaoCode { get; private set; }

    public bool IsEmpty
        =>
        AirportName is null && AirportIataCode is null && AirportIcaoCode is null;

    public AirportSearchFilter WithAirportName(string airportName)
    {
        if (!string.IsNullOrWhiteSpace(airportName))
            AirportName = airportName;

        return this;
    }

    public AirportSearchFilter WithAirportIataCode(string airportIataCode)
    {
        if (!string.IsNullOrWhiteSpace(airportIataCode) && airportIataCode.Length <= 3)
            AirportIataCode = airportIataCode;

        return this;
    }

    public AirportSearchFilter WithAirportIcaoCode(string airportIcaoCode)
    {
        if (!string.IsNullOrWhiteSpace(airportIcaoCode) && airportIcaoCode.Length <= 4)
            AirportIcaoCode = airportIcaoCode;

        return this;
    }
}