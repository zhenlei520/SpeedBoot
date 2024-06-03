// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.Internal.Interceptors;

internal static class SaveChangesInterceptor
{
    public static CustomConcurrentDictionary<string, string[]> PrimaryKeys = new CustomConcurrentDictionary<string, string[]>();

    public static void CurdAfter(IFreeSql freeSql, IServiceProvider serviceProvider, CurdAfterEventArgs args)
    {
        if (args.CurdType is not (CurdType.Insert or CurdType.Delete or CurdType.Update or CurdType.InsertOrUpdate))
            return;

        var saveChangesInterceptor = serviceProvider.GetServices<ISaveChangesInterceptor>().OrderBy(i => i.Order);
        if (saveChangesInterceptor.Any())
        {
            var identifier = args.Identifier.ToString();
            var entityState = args.CurdType.GetEntityState();

            var executeResult = args.ExecuteResult != null ? int.Parse(args.ExecuteResult.ToString()) : 0;
            var primaryKeys = PrimaryKeys.GetOrAdd(args.Table.DbName, (dbName) =>
            {
                var dbTableInfo = freeSql.DbFirst.GetTableByName(dbName);
                return dbTableInfo.Primarys.Select(primary => primary.Name).ToArray();
            });

            if (args.CurdType is CurdType.Insert)
            {
                foreach (var interceptor in saveChangesInterceptor)
                {
                    interceptor.SavedChanges(new SaveChangesCompletedEventData()
                    {
                        EventId = identifier,
                        EventName = "CurdAfter",
                        ContextId = identifier,
                        Duration = args.ElapsedMilliseconds,
                        Result = executeResult,
                        Entites = args.GetEntities(primaryKeys, executeResult, entityState)
                    });
                }
            }
        }
    }
}
