namespace TicketShopping.Domain.ValueObjects;

//TODO: consider using NetTopologySuite package for spatial and geometry data (if any calculations will be required)
public readonly record struct GeoLocation
{
    public GeoLocation(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException($"Latitude should be in the range of -90 - +90 inclusive");

        if (longitude < -180 || longitude > 180)
            throw new ArgumentException($"Longitude should be in the range of -180 - +180 inclusive");

        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; }
    public double Longitude { get; }
}
