// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Entities;

public interface IGenerateDomainEvents
{
    void AddDomainEvent(IDomainEvent domainEvent);

    void RemoveDomainEvent(IDomainEvent domainEvent);

    IEnumerable<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();
}
