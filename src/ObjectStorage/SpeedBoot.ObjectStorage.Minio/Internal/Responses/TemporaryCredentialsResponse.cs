// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

/// <summary>
/// Temporary Credentials
///
/// 临时凭证
/// </summary>
internal class TemporaryCredentialsResponse : CredentialsResponse
{
    /// <summary>
    /// 过期时间
    /// </summary>
    public DateTime? Expiration { get; set; }

    public TemporaryCredentialsResponse(string accessKeyId, string accessKeySecret, string? securityToken)
        : base(accessKeyId, accessKeySecret, securityToken)
    {
    }
}
