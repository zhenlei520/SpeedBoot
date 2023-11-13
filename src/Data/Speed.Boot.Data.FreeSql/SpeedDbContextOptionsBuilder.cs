// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.FreeSql;

public class SpeedDbContextOptionsBuilder
{
    public Type DbContextType { get; }

    public Action<IServiceProvider, FreeSqlBuilder>? OptionsAction { get; set; }

    public SpeedDbContextOptionsBuilder(Type dbContextType)
    {
        DbContextType = dbContextType;
    }
}
