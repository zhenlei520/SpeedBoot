// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.AspNetCore.Http;

public static class HttpContextExtensions
{
    public static string? GetRequestHeader(this HttpContext? httpContext, string key)
        => httpContext?.Request.Headers[key];

    public static Dictionary<string, string>? GetRequestHeaders(this HttpContext? httpContext)
        => httpContext?.Request.Headers.ToDictionary(item => item.Key, item => item.Value.ToString());
}
