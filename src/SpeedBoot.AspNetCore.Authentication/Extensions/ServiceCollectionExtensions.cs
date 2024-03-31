// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpeedBootIdentity(this IServiceCollection services)
        => services.AddSpeedBootIdentity<IdentityUser<Guid>, Guid>();

    public static IServiceCollection AddSpeedBootIdentity<TIdentityUser>(this IServiceCollection services, Action<IdentityClaimOptions> action)
        where TIdentityUser : class, IIdentityUser<Guid>, new()
        => services.AddSpeedBootIdentity<TIdentityUser, Guid>(action);

    private static IServiceCollection AddSpeedBootIdentity<TIdentityUser, TUserId>(this IServiceCollection services)
        where TIdentityUser : class, IIdentityUser<TUserId>, new()
        where TUserId : struct, IComparable, IEquatable<TUserId>
        => services.AddSpeedBootIdentity<TIdentityUser, TUserId>(_ => { });

    public static IServiceCollection AddSpeedBootIdentity<TIdentityUser, TUserId>(this IServiceCollection services, Action<IdentityClaimOptions> action)
        where TIdentityUser : class, IIdentityUser<TUserId>, new()
        where TUserId : struct, IComparable, IEquatable<TUserId>
    {
        if (!ServiceCollectionUtils.TryAdd<IdentityProvider>(services))
            return services;

        services.Configure(action);
        services.TryAddScoped<ICurrentPrincipalAccessor, HttpContextCurrentPrincipalAccessor>();
        services.TryAddScoped<UserContext<TIdentityUser, TUserId>>();
        services.TryAddScoped<IUserContext<TIdentityUser, TUserId>>(serviceProvider => serviceProvider.GetRequiredService<UserContext<TIdentityUser, TUserId>>());
        services.TryAddScoped<IUserSetter<TIdentityUser, TUserId>>(serviceProvider => serviceProvider.GetRequiredService<UserContext<TIdentityUser, TUserId>>());
        if (typeof(TUserId) != typeof(Guid))
            return services;

        var userContextType = typeof(UserContext<>).MakeGenericType(typeof(TIdentityUser));
        services.TryAddScoped(userContextType);
        services.TryAddScoped(typeof(IUserContext<>).MakeGenericType(typeof(TIdentityUser)), serviceProvider => serviceProvider.GetRequiredService(userContextType));
        services.TryAddScoped(typeof(IUserSetter<>).MakeGenericType(typeof(TIdentityUser)),serviceProvider => serviceProvider.GetRequiredService(userContextType));
        return services;
    }

    private sealed class IdentityProvider
    {
    }
}
