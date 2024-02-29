// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.DependencyInjection.Extensions;

public static class ServiceProviderExtensions
{
    public static TService? GetService<TService>(this IServiceProvider serviceProvider, string key) where TService : IService
        => serviceProvider.GetServices<TService>().Where(item => item.Key == key).FirstOrDefault();

    public static TService GetRequiredService<TService>(this IServiceProvider serviceProvider, string key) where TService : IService
        => serviceProvider.GetService<TService>(key) ?? throw new SpeedException($"Unregistered service: {typeof(TService).FullName}, key: {key}");
}
