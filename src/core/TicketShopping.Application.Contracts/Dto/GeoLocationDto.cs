namespace TicketShopping.Application.Contracts.Dto;

public readonly record struct GeoLocationDto
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}
