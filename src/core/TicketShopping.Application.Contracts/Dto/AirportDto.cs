namespace TicketShopping.Application.Contracts.Dto;

public record AirportDto
{
    public int? Id { get; init; }
    public required string Name { get; init; }
    public string? IataCode { get; init; }
    public string? IcaoCode { get; init; }
    public GeoLocationDto Location { get; init; }
}
