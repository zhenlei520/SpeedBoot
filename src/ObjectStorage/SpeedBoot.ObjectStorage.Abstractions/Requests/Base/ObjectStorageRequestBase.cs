// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// Object Storage Base
///
/// 对象存储基类
/// </summary>
public abstract class ObjectStorageRequestBase
{
    /// <summary>
    /// bucket name（桶名称（空间名称））
    /// </summary>
    public string? BucketName { get; set; }

    /// <summary>
    /// file name（文件名）
    /// </summary>
    public string ObjectName { get; set; }

    protected ObjectStorageRequestBase() { }

    protected ObjectStorageRequestBase(string? bucketName, string objectName)
    {
        BucketName = bucketName;
        ObjectName = objectName;
    }
}
