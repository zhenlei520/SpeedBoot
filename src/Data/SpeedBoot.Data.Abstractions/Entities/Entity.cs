// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.Abstractions;

public abstract class Entity : IEntity, IEquatable<Entity>, IEquatable<object>
{
    public abstract IEnumerable<object> GetKeys();

    /// <inheritdoc/>
    public override string ToString()
    {
        var keys = GetKeys().ToArray();
        string connector = keys.Length > 1 ? Environment.NewLine : string.Empty;

        return $"{GetType().Name}:{connector}{string.Join(Environment.NewLine, string.Join(", ", GetKeys()))}";
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Entity other => other.GetKeys().Select(key => key).SequenceEqual(GetKeys().Select(key => key)),
            _ => false
        };
    }

    public bool Equals(Entity? other)
    {
        return other is not null && other!.GetKeys().Select(key => key).SequenceEqual(GetKeys().Select(key => key));
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return GetKeys().Aggregate(17, (current, key) => current * 23 + (key?.GetHashCode() ?? 0));
        }
    }

    public static bool operator ==(Entity? x, Entity? y)
    {
        if (x is null ^ y is null) return false;

        return x is null || x.Equals(y);
    }

    public static bool operator !=(Entity? x, Entity? y)
    {
        if (x is null ^ y is null) return true;

        if (x is null) return false;

        return !x.Equals(y);
    }
}

public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    public virtual TKey Id { get; set; } = default!;

    protected Entity()
    {
    }

    protected Entity(TKey id) : this() => Id = id;

    public override IEnumerable<object> GetKeys()
    {
        yield return ("Id", Id!);
    }
}
