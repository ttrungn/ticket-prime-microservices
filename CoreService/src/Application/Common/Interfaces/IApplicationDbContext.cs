using CoreService.Domain.Entities;

namespace CoreService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<SubCategory> SubCategories { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Organizer> Organizers { get; }
    DbSet<Event> Events { get; }
    DbSet<Review> Reviews { get; }
    DbSet<Seat> Seats { get; }
    DbSet<SeatSection> SeatSections { get; }
    DbSet<SeatSectionRow> SeatSectionRows { get; }
    DbSet<Venue> Venues { get; }
    DbSet<Ticket> Tickets { get; }
    DbSet<TicketType> TicketTypes { get; }
    DbSet<TypeService> TypeServices { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
