// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionDescriptorExtensions
{
    /// <summary>
    /// Returns whether the specified ServiceType exists in the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    public static bool Any<TService>(this IServiceCollection services)
        => services.Any(d => d.ServiceType == typeof(TService));
}
