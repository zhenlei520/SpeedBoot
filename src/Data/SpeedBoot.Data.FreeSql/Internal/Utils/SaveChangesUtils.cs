// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.Internal.Utils;

internal static class SaveChangesUtils
{
    public static CustomConcurrentDictionary<Type, string[]> PrimaryKeys = new CustomConcurrentDictionary<Type, string[]>();

    public static void CurdBefore(IFreeSql freeSql, IServiceProvider serviceProvider, CurdBeforeEventArgs args)
    {
        if (args.CurdType is not (CurdType.Insert))
            return;

        var saveChangesInterceptors = serviceProvider.GetServices<ISaveChangesInterceptor>().OrderBy(i => i.Order);
        if (!saveChangesInterceptors.Any())
            return;

        var identifier = args.Identifier.ToString();
        var entityState = args.CurdType.GetEntityState();

        var dbName = args.Table.DbName;
        var primaryKeys = PrimaryKeys.GetOrAdd(args.EntityType, (_) =>
        {
            var dbTableInfo = freeSql.DbFirst.GetTableByName(dbName);
            return dbTableInfo.Primarys.Select(primary => primary.Name).ToArray();
        });

        if (args.CurdType is CurdType.Insert)
        {
            var executeResult = args.DbParms.Length / args.Table.Columns.Count;
            foreach (var interceptor in saveChangesInterceptors)
            {
                interceptor.SavingChanges(new DbContextEventData()
                {
                    EventId = identifier,
                    EventName = "CurdAfter",
                    ContextId = identifier,
                    Result = null,
                    Entites = args.GetEntities(primaryKeys, executeResult, entityState)
                });
            }
        }
    }

    public static void CurdAfter(IFreeSql freeSql, IServiceProvider serviceProvider, CurdAfterEventArgs args)
    {
        if (args.CurdType is not (CurdType.Insert or CurdType.Delete or CurdType.Update or CurdType.InsertOrUpdate))
            return;

        var saveChangesInterceptors = serviceProvider.GetServices<ISaveChangesInterceptor>().OrderBy(i => i.Order);
        if (!saveChangesInterceptors.Any())
            return;

        var identifier = args.Identifier.ToString();
        var executeResult = args.ExecuteResult != null ? int.Parse(args.ExecuteResult.ToString() ?? "0") : 0;

        var dbName = args.Table.DbName;
        PrimaryKeys.TryAdd(args.EntityType, (_) =>
        {
            var dbTableInfo = freeSql.DbFirst.GetTableByName(dbName);
            return dbTableInfo.Primarys.Select(primary => primary.Name).ToArray();
        });

        var saveChangesInterceptor = serviceProvider.GetRequiredService<FreeSqlSaveChangesInterceptor>();
        saveChangesInterceptor.SaveChangesCompletedEventData = new SaveChangesCompletedEventData()
        {
            EventId = identifier,
            EventName = "CurdAfter",
            ContextId = identifier,
            Duration = args.ElapsedMilliseconds,
            Result = executeResult,
            Entites = new List<EntityInfo>()
        };
    }
}
