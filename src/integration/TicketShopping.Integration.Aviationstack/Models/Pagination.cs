using System.Text.Json.Serialization;

namespace TicketShopping.Integration.Aviationstack.Models;
internal class Pagination
{
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("count")]
    public int? Count { get; init; }

    [JsonPropertyName("total")]
    public int? Total { get; init; }
}