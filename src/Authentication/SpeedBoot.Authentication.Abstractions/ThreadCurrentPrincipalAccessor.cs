// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication.Abstractions;

public class ThreadCurrentPrincipalAccessor : ICurrentPrincipalAccessor
{
    public virtual ClaimsPrincipal? GetCurrentPrincipal() => Thread.CurrentPrincipal as ClaimsPrincipal;
}
