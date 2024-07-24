// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Events;

public class DomainEventBus : IDomainEventBus
{
    private readonly IEventBus? _eventBus;
    private readonly IIntegrationEventBus? _integrationEventBus;

    private readonly ConcurrentQueue<IDomainEvent> _eventQueue = new();

    public DomainEventBus(
        IEventBus? eventBus = null,
        IIntegrationEventBus? integrationEventBus = null)
    {
        _eventBus = eventBus;
        _integrationEventBus = integrationEventBus;
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        if (@event is IIntegrationEvent integrationEvent)
        {
            SpeedArgumentException.ThrowIfNull(_integrationEventBus);
             _integrationEventBus.Publish(integrationEvent);
        }
        else
        {
            SpeedArgumentException.ThrowIfNull(_eventBus);
            _eventBus.Publish(@event);
        }
    }

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        if (@event is IIntegrationEvent integrationEvent)
        {
            SpeedArgumentException.ThrowIfNull(_integrationEventBus);
            await _integrationEventBus.PublishAsync(integrationEvent, cancellationToken);
        }
        else
        {
            SpeedArgumentException.ThrowIfNull(_eventBus);
            await _eventBus.PublishAsync(@event, cancellationToken);
        }
    }

    public Task Enqueue<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        => EnqueueAsync(@event);

    public Task EnqueueAsync<TDomainEvent>(TDomainEvent @event)
        where TDomainEvent : IDomainEvent
    {
        _eventQueue.Enqueue(@event);
        return Task.CompletedTask;
    }

    public async Task PublishQueueAsync()
    {
        while (_eventQueue.TryDequeue(out IDomainEvent? @event))
        {
            await PublishAsync(@event);
        }
    }

    public Task<bool> AnyQueueAsync()
    {
        return Task.FromResult(_eventQueue.Count > 0);
    }
}
