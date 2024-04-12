using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly.Extensions.Http;
using Polly;
using TicketShopping.Integration.Aviationstack.Client;
using TicketShopping.Integration.Aviationstack.Settings;

namespace TicketShopping.Integration.Aviationstack.DI;
public static class AviationstackExtensions
{
    /// <summary>
    /// Aviationstack integration configuration. See <see cref="AviationstackExtensions"/> extension class
    /// </summary>
    public static IServiceCollection RegisterAviationstackIntegration(this IServiceCollection services)
    {
        ConfigureOptions(services);
        RegisterClient(services);

        return services;
    }

    private static void ConfigureOptions(IServiceCollection services)
    {
        services.AddOptions<AviationstackSettings>()
                .BindConfiguration(nameof(AviationstackSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }

    private static void RegisterClient(IServiceCollection services)
    {
        static IAsyncPolicy<HttpResponseMessage> getRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        services.AddHttpClient<IAviationstackClient, AviationstackClient>((client, sp) =>
        {
            var options = sp.GetRequiredService<IOptions<AviationstackSettings>>();
            client.BaseAddress = new Uri(options.Value.BaseUrl);

            return new AviationstackClient(options, client);
        })
        .AddPolicyHandler(getRetryPolicy());
    }
}