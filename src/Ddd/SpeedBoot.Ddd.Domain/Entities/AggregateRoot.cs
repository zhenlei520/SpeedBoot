// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Entities;

public abstract class AggregateRoot : Entity, IAggregateRoot, IGenerateDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public virtual void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public virtual void RemoveDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Remove(domainEvent);

    public virtual IEnumerable<IDomainEvent> GetDomainEvents()
        => _domainEvents;

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}

public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>, IGenerateDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot() : base()
    {
    }

    protected AggregateRoot(TKey id) : base(id)
    {
    }

    public virtual void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public virtual void RemoveDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Remove(domainEvent);

    public virtual IEnumerable<IDomainEvent> GetDomainEvents()
        => _domainEvents;

    public void ClearDomainEvents()
        => _domainEvents.Clear();
}
