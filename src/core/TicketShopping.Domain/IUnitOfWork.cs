namespace TicketShopping.Domain;

public interface IUnitOfWork : IDisposable
{
    Guid Uid { get; }
    Task<int> CommitAsync(CancellationToken cancellation);
}
