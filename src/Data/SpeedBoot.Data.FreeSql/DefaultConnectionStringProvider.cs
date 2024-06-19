// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql;

public class DefaultConnectionStringProvider : IConnectionStringProvider
{
    private readonly IOptionsSnapshot<ConnectionStrings> _options;

    public DefaultConnectionStringProvider(IOptionsSnapshot<ConnectionStrings> options)
    {
        _options = options;
    }

    public Task<string> GetConnectionStringAsync(string? name = null)
        => Task.FromResult(GetConnectionString(name));

    public string GetConnectionString(string? name = null)
    {
        return _options.Value.GetConnectionString(name ?? GlobalDataConfig.ConnectionString.DefaultConnectionStringName);
    }
}
