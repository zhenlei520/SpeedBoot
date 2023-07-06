// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAliyunStorage(this IServiceCollection services)
        => services.AddAliyunStorage(ConfigurationHelper.GetAliyunObjectStorageOptions());

    public static IServiceCollection AddAliyunStorage(
        this IServiceCollection services,
        AliyunObjectStorageOptions aliyunObjectStorageOptions)
    {
        if (!ServiceCollectionUtils.TryAdd<AliyunStorageProvider>(services))
            return services;

        services.TryAddSingleton<IAliyunAcsClientFactory, DefaultAliyunAcsClientFactory>();
        services.TryAddSingleton<IAliyunClientProvider>(serviceProvider =>
        {
            var aliyunClientProvider = new DefaultAliyunClientProvider(
                aliyunObjectStorageOptions,
                serviceProvider.GetRequiredService<IAliyunAcsClientFactory>());
            return aliyunClientProvider;
        });
        services.AddSingleton<IObjectStorageClient, DefaultObjectStorageClient>();
        services.AddSingleton<IObjectStorageClientContainer, DefaultObjectStorageClientContainer>();
        return services;
    }

    private sealed class AliyunStorageProvider
    {
    }
}
