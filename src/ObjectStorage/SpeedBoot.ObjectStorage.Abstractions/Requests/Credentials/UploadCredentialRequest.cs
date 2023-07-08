// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// 上传文件凭证
/// </summary>
public class UploadCredentialRequest : CredentialsRequestBase
{
    /// <summary>
    /// 过期时间，Unit：s
    /// -1：永不过期
    /// </summary>
    public int? Expiry { get; set; } = -1;

    public UploadCredentialRequest(string objectName)
        : this(null, objectName)
    {

    }

    public UploadCredentialRequest(string? bucketName, string objectName)
        : base(bucketName, objectName)
    {

    }
}
