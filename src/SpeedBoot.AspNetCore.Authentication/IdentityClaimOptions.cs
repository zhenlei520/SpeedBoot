// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication;

public class IdentityClaimOptions
{
    private readonly Dictionary<string, string> _mappings = new(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(IdentityUser<Guid>.Id), ClaimTypes.NameIdentifier }
    };

    public Func<(Type PropertyType, string Claim), (bool IsSucceed, object Result)>? ConvertTo { get; set; }

    public IdentityClaimOptions Mapping(string name, string claimType)
    {
        _mappings[name] = claimType;
        return this;
    }

    internal bool TryGetClaimType(string name, out string? claimType)
        => _mappings.TryGetValue(name, out claimType);

    internal bool TryGetClaimValue(Type propertyType, string claim, out object? value)
    {
        value = null;
        if (ConvertTo == null)
            return false;

        var res = ConvertTo.Invoke((propertyType, claim));
        value = res.Result;
        return res.IsSucceed;
    }
}
