// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Ddd.Domain.Entities.Auditing;

public interface IAuditEntity<out TUserId> : IEntity
{
    TUserId Creator { get; }

    DateTime CreationTime { get; }

    TUserId Modifier { get; }

    DateTime ModificationTime { get; }
}

public interface IAuditEntity<out TKey, out TUserId> : IAuditEntity<TUserId>, IEntity<TKey>
{
}
