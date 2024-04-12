using System.Text.Json.Serialization;

namespace TicketShopping.Integration.Aviationstack.Models.Airport;

public record Airport
{
    [JsonPropertyName("airport_name")]
    public required string AirportName { get; init; }

    [JsonPropertyName("iata_code")]
    public string? IataCode { get; init; }

    [JsonPropertyName("icao_code")]
    public string? IcaoCode { get; init; }

    [JsonPropertyName("latitude")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public double Longitude { get; init; }

    [JsonPropertyName("geoname_id")]
    public string? GeonameId { get; init; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    [JsonPropertyName("gmt")]
    public string? Gmt { get; init; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; init; }

    [JsonPropertyName("country_name")]
    public string? CountryName { get; init; }

    [JsonPropertyName("country_iso2")]
    public string? CountryIso2 { get; init; }

    [JsonPropertyName("city_iata_code")]
    public string? CityIataCode { get; init; }
}