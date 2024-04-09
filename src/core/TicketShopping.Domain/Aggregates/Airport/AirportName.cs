namespace TicketShopping.Domain.Aggregates.Airport;

public record AirportName
{
    public AirportName(string airportName)
    {
        ArgumentException.ThrowIfNullOrEmpty(airportName, nameof(airportName));
        Name = airportName;
    }

    public string Name { get; }
}
