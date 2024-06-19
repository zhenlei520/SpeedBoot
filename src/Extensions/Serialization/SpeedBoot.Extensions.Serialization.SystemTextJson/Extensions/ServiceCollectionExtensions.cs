// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemTextJson(
        this IServiceCollection services,
        JsonSerializerOptions? jsonSerializerOptions = null)
    {
        return services.AddSystemTextJson(SystemTextJsonConfig.DefaultKey, jsonSerializerOptions);
    }

    public static IServiceCollection AddSystemTextJson(
        this IServiceCollection services,
        string key,
        JsonSerializerOptions? jsonSerializerOptions)
    {
        if (!SerializationServiceCollectionRegistry.TryAdd(services, key))
            return services;

        services.AddSingleton<IJsonSerializer>(sp => new SpeedBoot.Extensions.Serialization.SystemTextJson.JsonSerializer(key, jsonSerializerOptions));
        return services;
    }
}
