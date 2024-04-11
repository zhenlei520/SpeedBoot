// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Abstracts;

public interface IIntegrationEvent : IEvent, IIntegrationEventTopic
{
    bool GetOutboxEnabled();

    void SetOutboxEnabled(bool outboxEnabled);
}
