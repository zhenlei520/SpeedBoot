﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Entities.Auditing;

public abstract class AuditAggregateRoot<TUserId> : AggregateRoot, IAuditAggregateRoot<TUserId>
{
    public TUserId Creator { get; protected set; } = default!;

    public DateTime CreationTime { get; protected set; }

    public TUserId Modifier { get; protected set; } = default!;

    public DateTime ModificationTime { get; set; }

    protected AuditAggregateRoot() => Initialize();

    private void Initialize()
    {
        this.CreationTime = this.GetCurrentTime();
        this.ModificationTime = this.GetCurrentTime();
    }

    protected virtual DateTime GetCurrentTime() => DateTime.UtcNow;
}

public abstract class AuditAggregateRoot<TKey, TUserId> : AggregateRoot<TKey>, IAuditAggregateRoot<TKey, TUserId>
{
    public TUserId Creator { get; protected set; } = default!;

    public DateTime CreationTime { get; protected set; }

    public TUserId Modifier { get; protected set; } = default!;

    public DateTime ModificationTime { get; protected set; }

    protected AuditAggregateRoot() : base()
    {
        Initialize();
    }

    protected AuditAggregateRoot(TKey id) : base(id)
    {
        Initialize();
    }

    private void Initialize()
    {
        this.CreationTime = this.GetCurrentTime();
        this.ModificationTime = this.GetCurrentTime();
    }

    protected virtual DateTime GetCurrentTime() => DateTime.UtcNow;
}
