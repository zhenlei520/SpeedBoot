// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication;

public class UserContext<TIdentityUser, TUserId> : UserContextBase<TIdentityUser, TUserId>
    where TIdentityUser : class, IIdentityUser<TUserId>, new()
    where TUserId : struct, IComparable, IEquatable<TUserId>
{
    private readonly static List<PropertyInfo> _propertyInfos;
    protected readonly ICurrentPrincipalAccessor CurrentPrincipalAccessor;
    protected readonly IOptions<IdentityClaimOptions> Options;

    static UserContext()
    {
        _propertyInfos = typeof(TIdentityUser).GetProperties().Where(p => p.CanWrite).ToList();
    }

    public UserContext(ICurrentPrincipalAccessor currentPrincipalAccessor, IOptions<IdentityClaimOptions> options)
    {
        CurrentPrincipalAccessor = currentPrincipalAccessor;
        Options = options;
    }

    protected override TIdentityUser? GetUser()
    {
        var claimsPrincipal = CurrentPrincipalAccessor.GetCurrentPrincipal();
        if (claimsPrincipal == null)
            return null;

        var user = Activator.CreateInstance<TIdentityUser>();
        foreach (var propertyInfo in _propertyInfos)
        {
            if (TryGetClaimValue(propertyInfo, claimsPrincipal.Claims.ToArray(), out var claimValue))
                propertyInfo.SetValue(user, claimValue);
        }
        return user;
    }

    bool TryGetClaimValue(PropertyInfo propertyInfo, Claim[] claims, out object? claimValue)
    {
        claimValue = null;
        if (!Options.Value.TryGetClaimType(propertyInfo.Name, out var claimType) || !claims.TryGet(p => p.Type == claimType, out var claim))
            return false;

        var type = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
        return claim != null && (TypeHelper.TryConvertTo(claim.Value, type, out claimValue) || Options.Value.TryGetClaimValue(propertyInfo.PropertyType, claim.Value, out claimValue));
    }
}

public class UserContext<TIdentityUser> : UserContext<TIdentityUser, Guid>
    where TIdentityUser : class, IIdentityUser<Guid>, new()
{
    public UserContext(ICurrentPrincipalAccessor currentPrincipalAccessor, IOptions<IdentityClaimOptions> options)
        : base(currentPrincipalAccessor, options)
    {
    }
}
