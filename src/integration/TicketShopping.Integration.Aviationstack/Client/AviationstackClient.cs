using Microsoft.Extensions.Options;
using System.Text.Json;
using TicketShopping.Integration.Aviationstack.Models.Airport;
using TicketShopping.Integration.Aviationstack.Settings;

namespace TicketShopping.Integration.Aviationstack.Client;

internal class AviationstackClient : IAviationstackClient
{
    private readonly AviationstackSettings _settings;
    private readonly HttpClient _httpClient;

    internal AviationstackClient(IOptions<AviationstackSettings> options, HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient, nameof(httpClient));
        ValidateOptions(options);

        _settings = options.Value;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Airport>> GetAirports(CancellationToken cancellation)
    {
        var relativePath = $"airports?access_key={_settings.AccessToken}";
        var request = new HttpRequestMessage(HttpMethod.Get, relativePath);

        var response = await _httpClient.SendAsync(request, cancellation)
                                        .ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellation).ConfigureAwait(false);
        var airportResponse = JsonSerializer.Deserialize<AirportResponse>(content)
            ?? throw new Exception("Failed to convert airports response");

        return airportResponse.Data ?? throw new Exception("Failed to fetch airports. Null data.");
    }

    private static void ValidateOptions(IOptions<AviationstackSettings> options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
        ArgumentNullException.ThrowIfNull(options.Value, nameof(options));

        if (!Uri.TryCreate(options.Value.BaseUrl, UriKind.Absolute, out _))
            throw new ArgumentException("Aviationstack url should be a valid absolute Uri. Check configuration file",
                                        nameof(options));

        if (string.IsNullOrWhiteSpace(options.Value.AccessToken))
            throw new ArgumentException("Aviationstack access token should be specified", nameof(options));
    }
}
