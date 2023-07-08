// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

/// <summary>
/// 得到文件上传凭证
/// </summary>
public class ObjectStorageRequest : ObjectStorageRequestBase
{
    /// <summary>
    /// 失效时间（以秒为单位），默认是7天，不得大于七天
    ///
    /// 默认为10分钟
    /// </summary>
    public int? Expiration { get; set; }

    protected ObjectStorageRequest(string? bucketName, string objectName)
        : base(bucketName, objectName)
    {
    }
}
