﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication;

public class HttpContextCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextCurrentPrincipalAccessor(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;

    public override ClaimsPrincipal? GetCurrentPrincipal()
        => _httpContextAccessor.HttpContext?.User ?? base.GetCurrentPrincipal();
}
