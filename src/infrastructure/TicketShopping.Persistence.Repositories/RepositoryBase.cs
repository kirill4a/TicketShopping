using Microsoft.EntityFrameworkCore;

namespace TicketShopping.Persistence.Repositories;

internal abstract class RepositoryBase<T> where T : class
{
    protected RepositoryBase(DbSet<T> set) => Set = set ?? throw new ArgumentNullException(nameof(set));

    protected DbSet<T> Set { get; }
}
