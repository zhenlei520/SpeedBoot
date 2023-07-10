// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// 下载文件凭证
/// </summary>
public class DownloadCredentialRequest : CredentialsRequestBase
{
    /// <summary>
    /// 过期时间
    /// -1：永不过期
    /// </summary>
    public int? Expiry { get; set; } = -1;

    public DownloadCredentialRequest(string objectName)
        : this(null, objectName)
    {

    }

    public DownloadCredentialRequest(string? bucketName, string objectName)
        : base(bucketName, objectName)
    {

    }
}
