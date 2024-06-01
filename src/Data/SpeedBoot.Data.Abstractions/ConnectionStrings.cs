// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

[Serializable]
public class ConnectionStrings: Dictionary<string, string>
{
    public string DefaultConnection
    {
        get => GetConnectionString(GlobalDataConfig.ConnectionString.DefaultConnectionStringName);
        set => this[GlobalDataConfig.ConnectionString.DefaultConnectionStringName] = value;
    }

    public ConnectionStrings() { }

    public ConnectionStrings(IEnumerable<KeyValuePair<string, string>> connectionStrings)
        : base(connectionStrings.ToDictionary()) { }

    protected ConnectionStrings(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {

    }

    public string GetConnectionString(string name)
    {
        return TryGetValue(name, out var connectionString) ? connectionString : string.Empty;
    }
}
