namespace TicketShopping.Domain.Aggregates;

/// <summary>
/// Base domain entity
/// </summary>
public abstract class EntityBase<TId> where TId : IEquatable<TId>
{
    protected EntityBase(TId id)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        Id = id;
    }
    public TId Id { get; }

    public static IEqualityComparer<EntityBase<TId>> IdEqualityComparer
        =>
        EqualityComparer<EntityBase<TId>>.Create((left, right)
            =>
            left is null
                ? right is null
                : right is not null && left.GetType() == right.GetType() && left.Id.Equals(right.Id));
}