// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.AspNetCore.Http;

public static class HttpContextAccessorExtensions
{
    public static string? GetHeader(this IHttpContextAccessor? httpContextAccessor, string key)
        => httpContextAccessor?.HttpContext.GetRequestHeader(key);
}
