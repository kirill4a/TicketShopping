using MediatR;
using TicketShopping.Application.Contracts.Dto;

namespace TicketShopping.Application.Contracts.Queries;
public record SearchAirportsQuery(string? searchQuery) : IRequest<IEnumerable<AirportDto>>;