// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Services;

public interface IDomainService
{
    IDomainEventBus EventBus { get; }
}
