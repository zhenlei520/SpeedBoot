﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Entities.Auditing;

public interface IAuditAggregateRoot<out TUserId> : IAuditEntity<TUserId>, IAggregateRoot
{

}

public interface IAuditAggregateRoot<TKey, out TUserId> : IAuditEntity<TUserId>, IAggregateRoot<TKey>
{

}
