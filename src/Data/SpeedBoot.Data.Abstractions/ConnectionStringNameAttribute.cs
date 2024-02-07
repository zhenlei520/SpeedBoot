// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

[AttributeUsage(AttributeTargets.Class)]
public class ConnectionStringNameAttribute : Attribute
{
    public string Name { get; set; }

    public DbOperateTypes DbOperateTypes { get; set; }

    public ConnectionStringNameAttribute(string name = "", DbOperateTypes dbOperateType = DbOperateTypes.Write | DbOperateTypes.Read)
    {
        Name = name;
        DbOperateTypes = dbOperateType;
    }

    private static readonly List<DbContextNameRelationOptions> DbContextNameRelationOptions = new();

    public static string GetConnStringName<T>() => GetConnStringName(typeof(T));

    public static string GetConnStringName(Type type)
    {
        var options = DbContextNameRelationOptions.FirstOrDefault(c => c.DbContextType == type);
        if (options != null) return options.Name;

        var name = type.GetTypeInfo().GetCustomAttribute<ConnectionStringNameAttribute>()?.Name;
        if (name.IsNullOrWhiteSpace())
            name = ConnectionStrings.DEFAULT_CONNECTION_STRING_NAME;

        DbContextNameRelationOptions.Add(new DbContextNameRelationOptions(name!, type));
        return name!;
    }
}
