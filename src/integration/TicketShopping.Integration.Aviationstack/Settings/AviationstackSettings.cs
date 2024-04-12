using System.ComponentModel.DataAnnotations;

namespace TicketShopping.Integration.Aviationstack.Settings;
public class AviationstackSettings
{
    [Required]
    [Url]
    public string BaseUrl { get; init; }

    [Required]
    public string AccessToken { get; init; }
}