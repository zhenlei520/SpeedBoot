﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// 上传文件
/// </summary>
public class PutObjectStorageRequest : ObjectStorageRequestBase
{
    /// <summary>
    /// File Stream
    ///
    /// 文件数据流
    /// </summary>
    public Stream Stream { get; set; }

    /// <summary>
    /// enable file overwrite（启用文件覆盖）
    ///
    /// default: true
    /// </summary>
    public bool? EnableOverwrite { get; set; }

    public PutObjectStorageRequest() { }

    public PutObjectStorageRequest(string bucketName, string objectName, Stream stream, bool? enableOverwrite = null)
        : base(bucketName, objectName)
    {
        Stream = stream;
        EnableOverwrite = enableOverwrite;
    }
}
