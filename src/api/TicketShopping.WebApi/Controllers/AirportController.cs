using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketShopping.Application.Contracts.Queries;

namespace TicketShopping.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AirportController : ControllerBase
{
    private readonly IMediator _mediator;

    public AirportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ping")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Ping(CancellationToken cancellation)
    {
        var result = await Task.FromResult("Pong");
        return Ok(result);
    }

    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string? query, CancellationToken cancellation)
    {
        var trimmedQuery = query?.Trim();

        if (!string.IsNullOrWhiteSpace(trimmedQuery) && trimmedQuery.Trim().Length < 2)
            return BadRequest("Search query should contain at least 2 characters");

        var searchQuery = new SearchAirportsQuery(trimmedQuery);
        var airports = await _mediator.Send(searchQuery, cancellation);
        return Ok(airports);
    }
}
