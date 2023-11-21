// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.Abstractions;

[Serializable]
public class ConnectionStrings: Dictionary<string, string>
{
    public const string DEFAULT_SECTION = "ConnectionStrings";

    public const string DEFAULT_CONNECTION_STRING_NAME = "DefaultConnection";

    public string DefaultConnection
    {
        get => GetConnectionString(DEFAULT_CONNECTION_STRING_NAME);
        set => this[DEFAULT_CONNECTION_STRING_NAME] = value;
    }

    public ConnectionStrings() { }

    public ConnectionStrings(IEnumerable<KeyValuePair<string, string>> connectionStrings)
        : base(EnumerableExtensions.ToDictionary(connectionStrings)) { }

    protected ConnectionStrings(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {

    }

    public string GetConnectionString(string name)
    {
        return TryGetValue(name, out var connectionString) ? connectionString : string.Empty;
    }
}
