// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Abstracts;

public interface ILocalEventBus : IEventBus
{
    TResult Publish<TEvent, TResult>(TEvent @event)
        where TEvent : IEvent;

    Task<TResult> PublishAsync<TEvent, TResult>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent;
}
