using System.Reflection;
using CoreService.Application.Common.Interfaces;
using CoreService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreService.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<SubCategory> SubCategories => Set<SubCategory>();

    public DbSet<Organizer> Organizers => Set<Organizer>();

    public DbSet<Event> Events => Set<Event>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<Seat> Seats => Set<Seat>();

    public DbSet<SeatSection> SeatSections => Set<SeatSection>();

    public DbSet<SeatSectionRow> SeatSectionRows => Set<SeatSectionRow>();

    public DbSet<Venue> Venues => Set<Venue>();

    public DbSet<Ticket> Tickets => Set<Ticket>();

    public DbSet<TicketType> TicketTypes => Set<TicketType>();

    public DbSet<TypeService> TypeServices => Set<TypeService>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

