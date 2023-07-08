// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMinio(this IServiceCollection services)
        => services.AddMinio(ConfigurationHelper.GetMinioObjectStorageOptions());

    public static IServiceCollection AddMinio(
        this IServiceCollection services,
        MinioObjectStorageOptions minioObjectStorageOptions)
    {
        if (!ServiceCollectionUtils.TryAdd<MinioStorageProvider>(services))
            return services;

        services.TryAddSingleton<IMinioClientProvider>(serviceProvider =>
        {
            var minioClientProvider = new DefaultMinioClientProvider(minioObjectStorageOptions);
            return minioClientProvider;
        });
        services.AddSingleton<IObjectStorageClient, DefaultObjectStorageClient>();
        return services;
    }

    private sealed class MinioStorageProvider
    {
    }
}
