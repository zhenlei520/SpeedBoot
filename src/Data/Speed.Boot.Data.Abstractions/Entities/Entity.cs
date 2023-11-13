// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.Abstractions;

public abstract class Entity : IEntity, IEquatable<Entity>, IEquatable<object>
{
    public abstract IEnumerable<(string Name, object Value)> GetKeys();

    /// <inheritdoc/>
    public override string ToString()
    {
        var keys = GetKeys().ToArray();
        string connector = keys.Length > 1 ? Environment.NewLine : string.Empty;

        return $"{GetType().Name}:{connector}{string.Join(Environment.NewLine, keys.Select(key => $"{key.Name}={key.Value}"))}";
    }

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            null => false,
            Entity other => other.GetKeys().Select(key => key.Value).SequenceEqual(GetKeys().Select(key => key.Value)),
            _ => false
        };
    }

    public bool Equals(Entity? other)
    {
        return other is not null && other!.GetKeys().Select(key => key.Value).SequenceEqual(GetKeys().Select(key => key.Value));
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return GetKeys().Aggregate(17, (current, key) => current * 23 + (key.Value?.GetHashCode() ?? 0));
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

    public override IEnumerable<(string Name, object Value)> GetKeys()
    {
        yield return ("Id", Id!);
    }
}
