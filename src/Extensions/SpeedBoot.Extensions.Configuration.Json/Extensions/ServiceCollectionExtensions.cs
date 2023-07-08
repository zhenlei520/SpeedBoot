// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJsonConfiguration(
        this IServiceCollection services,
        string jsonFile,
        bool optional = false,
        bool reloadOnChange = false)
        => services.AddJsonConfiguration(Directory.GetCurrentDirectory(), jsonFile, optional, reloadOnChange);

    public static IServiceCollection AddJsonConfiguration(
        this IServiceCollection services,
        string basePath,
        string jsonFile,
        bool optional = false,
        bool reloadOnChange = false)
    {
        var newConfiguration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile(jsonFile, optional, reloadOnChange)
            .Build();

        var configuration = services.GetSingletonInstance<IConfiguration>();
        switch (configuration)
        {
            case null:
                services.AddSingleton<IConfiguration>(newConfiguration);
                break;
#if NET6_0_OR_GREATER
            case ConfigurationManager configurationManager:
                configurationManager.AddConfiguration(newConfiguration);
                break;
#endif
#if NETCOREAPP3_0_OR_GREATER
            case IConfigurationBuilder configurationBuilder:
                configurationBuilder.AddConfiguration(newConfiguration);
                break;
#endif
            default:
                throw new NotSupportedException("Please use a higher version of net framework");

        }
        return services;
    }
}
