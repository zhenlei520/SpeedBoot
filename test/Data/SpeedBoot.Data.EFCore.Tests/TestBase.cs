// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore.Tests;

public class TestBase
{
    /// <summary>
    /// Service Collections
    /// </summary>
    protected IServiceCollection Services;

    protected TestBase()
    {
        Services = new ServiceCollection();
        // todo: If you do not reference SpeedBoot.Extensions.DependencyInjection, you must add
        // App.Instance.RebuildRootServiceProvider ??= s => s.BuildServiceProvider();

        var applicationBuilder = Services.AddSpeedBoot(options =>
        {
            options.Assemblies = AppDomain.CurrentDomain.GetAssemblies().Concat(new Assembly[]
            {
                typeof(DefaultAutoInjectProvider).Assembly
            }).ToArray();
        });

        applicationBuilder.Build();
        applicationBuilder.SetRootServiceProvider(Services.BuildServiceProvider());
    }
}
