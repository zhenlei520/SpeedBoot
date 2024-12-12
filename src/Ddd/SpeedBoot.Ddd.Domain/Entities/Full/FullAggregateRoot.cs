// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Entities.Full;

public abstract class FullAggregateRoot<TUserId>
    : AuditAggregateRoot<TUserId>, IFullAggregateRoot<TUserId>
{
    public bool IsDeleted { get; protected set; }
}

public abstract class FullAggregateRoot<TKey, TUserId>
    : AuditAggregateRoot<TKey, TUserId>, IFullAggregateRoot<TKey, TUserId>
{
    public bool IsDeleted { get; protected set; }

    protected FullAggregateRoot() : base()
    {
    }

    protected FullAggregateRoot(TKey id) : base(id)
    {
    }
}

