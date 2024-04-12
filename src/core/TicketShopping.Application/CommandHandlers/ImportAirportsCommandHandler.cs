using MediatR;
using Microsoft.Extensions.Logging;
using TicketShopping.Application.Contracts.Commands;
using TicketShopping.Application.Contracts.Dto;
using TicketShopping.Domain;
using TicketShopping.Domain.Aggregates.Airport;
using TicketShopping.Domain.ValueObjects;
using TicketShopping.Integration.Aviationstack.Client;
using DomainAirport = TicketShopping.Domain.Aggregates.Airport.Airport;
using ExternalAirport = TicketShopping.Integration.Aviationstack.Models.Airport.Airport;

namespace TicketShopping.Application.CommandHandlers;

internal class ImportAirportsCommandHandler : IRequestHandler<ImportAirportsCommand, ImportAirportsResult>
{
    private readonly IAviationstackClient _aviationstackClient;
    private readonly IAirportRepository _airportRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ImportAirportsCommandHandler> _logger;

    public ImportAirportsCommandHandler(
        IAviationstackClient aviationstackClient,
        IAirportRepository airportRepository,
        IUnitOfWork unitOfWork,
        ILogger<ImportAirportsCommandHandler> logger)
    {
        _aviationstackClient = aviationstackClient ?? throw new ArgumentNullException(nameof(aviationstackClient));
        _airportRepository = airportRepository ?? throw new ArgumentNullException(nameof(airportRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ImportAirportsResult> Handle(ImportAirportsCommand command, CancellationToken cancellation)
    {
        var externalAirports = await _aviationstackClient.GetAirports(cancellation).ConfigureAwait(false)
            ?? throw new Exception("Cant't get airports from Aviationstack");

        var domainAirports = externalAirports.Select(MapToDomain).ToList();
        _airportRepository.AddRange(domainAirports);

        try
        {
            var rowsAffected = await _unitOfWork.CommitAsync(cancellation).ConfigureAwait(false);
            return new(rowsAffected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to import airports. UnitOfWork id: {@unitOfWorkId}", _unitOfWork.Uid);
            throw;
        }
    }

    private DomainAirport MapToDomain(ExternalAirport extAirport)
    {
        ArgumentNullException.ThrowIfNull(extAirport);

        return new DomainAirport(new AirportId(),
                                 new GeoLocation(extAirport.Latitude, extAirport.Longitude),
                                 new AirportName(extAirport.AirportName),
                                 extAirport.IataCode is null ? null : new AirportIata(extAirport.IataCode),
                                 extAirport.IcaoCode is null ? null : new AirportIcao(extAirport.IcaoCode));
    }
}