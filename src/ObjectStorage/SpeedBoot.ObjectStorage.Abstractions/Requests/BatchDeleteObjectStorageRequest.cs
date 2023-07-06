// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

public class BatchDeleteObjectStorageRequest
{
    /// <summary>
    /// bucket name（桶名（空间名））
    /// </summary>
    public string BucketName { get; set; }

    /// <summary>
    /// A collection of filenames to be deleted
    ///
    /// 待删除的文件集合
    /// </summary>
    public List<string> ObjectNames { get; set; }

    public BatchDeleteObjectStorageRequest() { }

    public BatchDeleteObjectStorageRequest(string bucketName, List<string> objectNames)
    {
        BucketName = bucketName;
        ObjectNames = objectNames;
    }

    public BatchDeleteObjectStorageRequest(string bucketName, params string[] objectNames)
        : this(bucketName, objectNames.ToList())
    {
    }
}
