using CoreService.Application.Common.Interfaces;
using CoreService.Application.Venues.Commands.DTOs;
using CoreService.Domain.Entities;

namespace CoreService.Application.Venues.Commands.ImportVenue;

public record ImportVenueCommand : IRequest<ImportVenueResult>
{
    public string Name { get; init; } = null!;
    public List<CategoryDto> Categories { get; init; } = new();
    public List<ZoneDto> Zones { get; init; } = new();
    public SizeDto Size { get; init; } = null!;
}

public record ImportVenueResult(Guid VenueId);

public class ImportVenueCommandValidator : AbstractValidator<ImportVenueCommand>
{
    public ImportVenueCommandValidator()
    {
    }
}

public class ImportVenueCommandHandler : IRequestHandler<ImportVenueCommand, ImportVenueResult>
{
    private readonly IApplicationDbContext _context;

    public ImportVenueCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ImportVenueResult> Handle(ImportVenueCommand importVenueCommand, CancellationToken cancellationToken)
    {
        var ticketTypes = importVenueCommand.Categories
            .Select(c => new TicketType
            {
                Id = Guid.NewGuid(),
                Name = c.Name,
                Color = c.Color
            })
            .ToList();
        var seatSections = importVenueCommand.Zones
            .Select(zone =>
            {
                var seatSectionRows = zone.Rows.Select(row =>
                {
                    var seats = row.Seats.Select(seat =>
                    {
                        var ticketType = ticketTypes.FirstOrDefault(t => t.Name == seat.Category)
                                         ?? throw new InvalidOperationException($"Category '{seat.Category}' not found.");

                        return new Seat
                        {
                            SeatNumber = seat.SeatNumber,
                            SeatCode = seat.SeatGuid,
                            PositionX = seat.Position.X,
                            PositionY = seat.Position.Y,
                            Radius = seat.Radius,
                            TicketType = ticketType
                        };
                    }).ToList();

                    return new SeatSectionRow
                    {
                        RowNumber = row.RowNumber,
                        PositionX = row.Position.X,
                        PositionY = row.Position.Y,
                        RowNumberPosition = row.RowNumberPosition,
                        Seats = seats
                    };
                }).ToList();

                return new SeatSection
                {
                    Name = zone.Name,
                    PositionX = zone.Position.X,
                    PositionY = zone.Position.Y,
                    SeatSectionRows = seatSectionRows
                };
            }).ToList();

        var venue = new Venue()
        {
            OrganizerId = Guid.Parse("d6ad1aae-2301-4dcd-b54e-637fe068d7c4"),
            Name = importVenueCommand.Name,
            Width = importVenueCommand.Size.Width,
            Height = importVenueCommand.Size.Height,
            SeatSections = seatSections,
            TicketTypes = ticketTypes
        };

        await _context.Venues.AddAsync(venue, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ImportVenueResult(venue.Id);
    }
}
