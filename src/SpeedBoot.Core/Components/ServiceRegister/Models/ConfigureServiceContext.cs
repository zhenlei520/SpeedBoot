// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot;

public class ConfigureServiceContext
{
    public IServiceCollection Services { get; }

    public IConfiguration? Configuration { get; }

    public IEnvironment Environment { get; }

    public ConfigureServiceContext(IServiceCollection services)
    {
        Services = services;
    }

    public ConfigureServiceContext(IServiceCollection services, IEnvironment environment, IConfiguration? configuration)
        : this(services)
    {
        Configuration = configuration;
        Environment = environment;
    }
}
