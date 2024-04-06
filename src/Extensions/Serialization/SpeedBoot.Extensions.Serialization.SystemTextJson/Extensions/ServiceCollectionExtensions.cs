// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSystemTextJson(
        this IServiceCollection services,
        JsonSerializerOptions? jsonSerializerOptions = null)
    {
        if (!ServiceCollectionUtils.TryAdd<SerializationProvider>(services))
            return services;

        services.AddSingleton<IJsonSerializer>(sp => new SpeedBoot.Extensions.Serialization.SystemTextJson.JsonSerializer(jsonSerializerOptions));
        return services;
    }

    private sealed class SerializationProvider
    {
    }
}
