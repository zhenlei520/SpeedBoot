// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Events;

public interface IDomainEventBus : IEventBus
{
    Task EnqueueAsync<TDomainEvent>(TDomainEvent @event)
        where TDomainEvent : IDomainEvent;

    Task PublishQueueAsync();

    Task<bool> AnyQueueAsync();
}
