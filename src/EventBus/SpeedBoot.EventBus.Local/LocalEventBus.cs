// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class LocalEventBus: ILocalEventBus
{
    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        throw new NotImplementedException();
    }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        throw new NotImplementedException();
    }

    public TResult Publish<TEvent, TResult>(TEvent @event) where TEvent : IEvent
    {
        throw new NotImplementedException();
    }

    public Task<TResult> PublishAsync<TEvent, TResult>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        throw new NotImplementedException();
    }
}
