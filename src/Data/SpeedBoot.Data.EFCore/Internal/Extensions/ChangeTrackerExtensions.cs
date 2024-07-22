// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.EntityFrameworkCore;

internal static class ChangeTrackerExtensions
{
    public static List<EntityInfo> GetEntites(this ChangeTracker changeTracker)
    {
        return changeTracker.Entries()
            .Where(entry => entry.State != EntityState.Detached && entry.State != EntityState.Unchanged)
            .Select(entry =>
            {
                var entryInfo = new EntityInfo()
                {
                    EntityState = entry.State.GetEntityState(),
                    EntityType = entry.Entity.GetType(),
                    PropertyInfos = entry.Metadata.GetProperties().Select(property =>
                    {
                        var propertyEntry = entry.Property(property.Name);
                        var entityPropertyInfo = new EntityPropertyInfo()
                        {
                            IsPrimaryKey = property.IsPrimaryKey(),
                            NewValue = propertyEntry.CurrentValue,
                            OldValue = propertyEntry.OriginalValue,
                            PropertyName = property.Name,
                            PropertyType = property.PropertyInfo.PropertyType
                        };
                        return entityPropertyInfo;
                    }).ToList()
                };
                return entryInfo;
            }).ToList();
    }
}
