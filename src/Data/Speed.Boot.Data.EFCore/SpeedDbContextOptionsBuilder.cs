// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Speed.Boot.Data.EFCore;

public class SpeedDbContextOptionsBuilder
{
    public Action<IServiceProvider, DbContextOptionsBuilder>? OptionsAction { get; set; }

    public Type DbContextType { get; set; }
}
