// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace FreeSql.Aop;

internal static class CurdBeforeEventArgsExtensions
{
    public static List<EntityInfo> GetEntities(
        this CurdBeforeEventArgs args,
        string[] primaryKeys,
        int executeResult,
        EntityState entityState)
    {
        var list = new List<EntityInfo>();
        for (var index = 0; index < executeResult; index++)
        {
            list.Add(new EntityInfo()
            {
                EntityState = entityState,
                EntityType = args.EntityType,
                PropertyInfos = args.Table.Properties.Select(property =>
                {
                    var parameters = args.DbParms.Where(p => p.ParameterName == $"@{property.Key}_{index}").ToList();
                    return new EntityPropertyInfo()
                    {
                        IsPrimaryKey = primaryKeys.Contains(property.Key),
                        PropertyName = property.Key,
                        PropertyType = property.Value.PropertyType,
                        OldValue = parameters.Where(p => p.SourceVersion == DataRowVersion.Original).Select(p => p.Value).FirstOrDefault(),
                        NewValue = parameters.Where(p => p.SourceVersion == DataRowVersion.Current).Select(p => p.Value).FirstOrDefault()
                    };
                }).ToList()
            });
        }

        return list;
    }
}
