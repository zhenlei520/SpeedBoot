// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

/// <summary>
/// ip extensions methods
/// </summary>
public static class IpUtils
{
    /// <summary>
    /// Get the requested ip address
    /// 获取请求的ip地址
    /// </summary>
    /// <param name="keys">获取 ip 的 header值</param>
    /// <returns></returns>
    public static string? GetRequestIp(params string[] keys)
    {
        if (keys.Length == 0)
        {
            keys = new[] { "HTTP_X_ForWARDED_For", "REMOTE_ADDR" };
        }
        return keys.Select(key => App.Instance.GetHttpContext().GetRequestHeader(key)).FirstOrDefault(ip => !ip.IsNullOrWhiteSpace());
    }
}
