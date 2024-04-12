using Microsoft.EntityFrameworkCore;
using TicketShopping.Domain;
using DbAirport = TicketShopping.Persistence.Entities.Models.Airport;

namespace TicketShopping.Persistence.Repositories;
public class TicketShoppingDbContext : DbContext, IUnitOfWork
{
    public TicketShoppingDbContext(DbContextOptions<TicketShoppingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbAirport).Assembly);
    }

    public Guid Uid => ContextId.InstanceId;

    public Task<int> CommitAsync(CancellationToken cancellation) => SaveChangesAsync(cancellation);

    public DbSet<DbAirport> Airports => Set<DbAirport>();
}