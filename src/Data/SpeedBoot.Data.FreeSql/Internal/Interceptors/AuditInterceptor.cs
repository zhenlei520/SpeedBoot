// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.Internal.Interceptors;

internal class AuditInterceptor
{
    public List<EntityInfo> Entites { get; set; }

    public AuditInterceptor()
    {
        Entites = new();
    }

    public void SetEntity(DbContext.EntityChangeReport.ChangeInfo changeInfo)
    {
        var entityInfo = new EntityInfo()
        {
            EntityState = changeInfo.Type.GetEntityState(),
            EntityType = changeInfo.EntityType,
            PropertyInfos = new List<EntityPropertyInfo>()//todo: Build a delegate through an expression tree and pass in an object to get PropertyInfos
        };
        Entites.Add(entityInfo);
    }
}
