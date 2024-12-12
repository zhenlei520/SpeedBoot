// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Services;

public class DomainService : IDomainService
{
    public IDomainEventBus EventBus { get; private set; }

    public DomainService()
    {
        EventBus = null!;
    }

    public DomainService(IDomainEventBus eventBus) => EventBus = eventBus;

    public void SetDomainEventBus(IDomainEventBus domainEventBus)
    {
        EventBus = domainEventBus;
    }
}
