// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.EntityFrameworkCore;

internal static class EntityStateExtensions
{
    public static SpeedBoot.Data.Abstractions.EntityState? GetEntityState(this EntityState entityState)
    {
        return entityState switch
        {
            EntityState.Added => SpeedBoot.Data.Abstractions.EntityState.Added,
            EntityState.Modified => SpeedBoot.Data.Abstractions.EntityState.Modified,
            EntityState.Deleted => SpeedBoot.Data.Abstractions.EntityState.Deleted, //todo: Subsequent support for soft deletion
            _ => null
        };
    }
}
