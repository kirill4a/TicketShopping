namespace TicketShopping.Domain.Aggregates.Airport;

public record AirportIcao
{
    public AirportIcao(string icaoAirportCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(icaoAirportCode, nameof(icaoAirportCode));
        if (icaoAirportCode.Length != 4)
            throw new ArgumentException($"ICAO airport code '{icaoAirportCode}' is not valid", nameof(icaoAirportCode));

        Code = icaoAirportCode;
    }
    public string Code { get; }
}
