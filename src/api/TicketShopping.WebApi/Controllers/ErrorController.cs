using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;

namespace TicketShopping.WebApi.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [Route("error")]
    public async Task<IActionResult> ErrorAsync()
    {
        var traceId = HttpContext.TraceIdentifier;

        var request = HttpContext.Features.Get<IHttpRequestFeature>()
            ?? throw new NullReferenceException($"Couldn\'t extract request from HttpContext '{traceId}'");

        var exception = (HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error)
            ?? throw new NullReferenceException($"Couldn\'t extract exception from HttpContext '{traceId}'");

        var url = request.RawTarget;
        var method = request.Method;

        string body;
        request.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(request.Body, Encoding.UTF8, true, -1, true);
        body = await reader.ReadToEndAsync();

        _logger.LogError(exception,
            "Unhandled exception in {@assemblyName}. URL: {@url}. Method: {@method}. Request body: {@body}",
            Assembly.GetExecutingAssembly().GetName().Name, url, method, body);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}