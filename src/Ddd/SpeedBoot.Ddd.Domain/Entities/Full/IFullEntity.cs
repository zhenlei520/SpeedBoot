// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using SpeedBoot.Data.Abstractions.DataFiltering;

namespace SpeedBoot.Ddd.Domain.Entities.Full;

public interface IFullEntity<out TUserId> : IAuditEntity<TUserId>, ISoftDelete
{

}

public interface IFullEntity<TKey, out TUserId> : IAuditEntity<TKey, TUserId>, ISoftDelete
{

}
