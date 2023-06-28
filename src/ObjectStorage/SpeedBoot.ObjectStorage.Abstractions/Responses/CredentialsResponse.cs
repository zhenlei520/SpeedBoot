// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// Credentials Or Temporary Credentials
///
/// 凭证或者临时凭证
/// </summary>
public class CredentialsResponse
{
    public string AccessKeyId { get; }

    public string AccessKeySecret { get; }

    /// <summary>
    /// SecurityToken（Temporary Credentials Required）
    ///
    /// 安全令牌（临时凭证需要）
    /// </summary>
    public string? SecurityToken { get; }

    public CredentialsResponse(string accessKeyId, string accessKeySecret, string? securityToken)
    {
        AccessKeyId = accessKeyId;
        AccessKeySecret = accessKeySecret;
        SecurityToken = securityToken;
    }
}
