using MediatR;
using TicketShopping.Application.Contracts.Dto;

namespace TicketShopping.Application.Contracts.Commands;
public readonly record struct ImportAirportsCommand() : IRequest<ImportAirportsResult>;