// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Entities.Full;

public abstract class FullEntity<TUserId>
    : AuditEntity<TUserId>, IFullEntity<TUserId>
{
    public bool IsDeleted { get; protected set; }
}

public abstract class FullEntity<TKey, TUserId>
    : AuditEntity<TKey, TUserId>, IFullEntity<TKey, TUserId>
{
    public bool IsDeleted { get; protected set; }

    protected FullEntity() : base()
    {
    }

    protected FullEntity(TKey id) : base(id)
    {
    }
}
