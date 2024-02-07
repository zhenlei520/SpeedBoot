// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.Abstractions;

public interface IEntity
{
    IEnumerable<object> GetKeys();
}

public interface IEntity<out TKey> : IEntity
{
    TKey Id { get; }
}
