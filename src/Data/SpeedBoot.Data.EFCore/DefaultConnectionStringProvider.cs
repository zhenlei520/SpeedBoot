// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore;

public class DefaultConnectionStringProvider: IConnectionStringProvider
{
    private readonly IOptionsSnapshot<ConnectionStrings> _options;

    public DefaultConnectionStringProvider(IOptionsSnapshot<ConnectionStrings> options)
    {
        _options = options;
    }

    public Task<string> GetConnectionStringAsync(string name = ConnectionStrings.DEFAULT_CONNECTION_STRING_NAME)
        => Task.FromResult(GetConnectionString(name));

    public string GetConnectionString(string name = ConnectionStrings.DEFAULT_CONNECTION_STRING_NAME)
    {
        return _options.Value.GetConnectionString(name);
    }
}
