// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.Serialization.SystemTextJson.Tests;

public class TestBase
{
    private readonly SpeedBootApplicationBuilder _applicationBuilder;
    protected readonly IServiceCollection Services;

    public TestBase()
    {
        Services = new ServiceCollection();
        _applicationBuilder =Services.AddSpeedBoot(options =>
        {
            options.Assemblies = AppDomain.CurrentDomain.GetAssemblies().Concat(new Assembly[]
            {
                typeof(DefaultAutoInjectProvider).Assembly,
                typeof(JsonSerializer).Assembly
            }).ToArray();
        });
    }

    protected void Build()
    {
        _applicationBuilder.Build();
        _applicationBuilder.SetRootServiceProvider(Services.BuildServiceProvider());
    }
}
