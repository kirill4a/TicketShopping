using System.Text.Json.Serialization;

namespace TicketShopping.Integration.Aviationstack.Models.Airport;

internal class AirportResponse
{
    [JsonPropertyName("pagination")]
    public Pagination? Pagination { get; init; }

    [JsonPropertyName("data")]
    public IEnumerable<Airport>? Data { get; init; }
}