// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public interface IStrategyExecutor
{
    public void Execute<TEvent>(DispatchRecord dispatchRecord, TEvent @event)
        where TEvent : IEvent;

    public Task ExecuteAsync<TEvent>(DispatchRecord dispatchRecord, TEvent @event, CancellationToken cancellationToken)
        where TEvent : IEvent;
}
