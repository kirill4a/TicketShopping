namespace TicketShopping.Persistence.Entities.Models;

public class Airport
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Iata { get; init; }
    public string? Icao { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}
