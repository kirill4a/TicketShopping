using Microsoft.EntityFrameworkCore;
using DbAirport = TicketShopping.Persistence.Entities.Models.Airport;

namespace TicketShopping.Persistence.Repositories;

public class TicketShoppingDbContext : DbContext
{
    public TicketShoppingDbContext(DbContextOptions<TicketShoppingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbAirport).Assembly);
    }

    public DbSet<DbAirport> Airports => Set<DbAirport>();
}
