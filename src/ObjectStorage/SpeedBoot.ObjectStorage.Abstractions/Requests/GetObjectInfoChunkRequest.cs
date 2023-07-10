// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

public class GetObjectInfoChunkRequest : ObjectStorageRequestBase
{
    /// <summary>
    /// 开始下标
    /// </summary>
    public long Offset { get; set; }

    /// <summary>
    /// 读取长度
    /// </summary>
    public long Length { get; set; }

    public GetObjectInfoChunkRequest() { }

    public GetObjectInfoChunkRequest(string objectName, long offset, long length)
        : this(null, objectName, offset, length)
    {
    }

    public GetObjectInfoChunkRequest(string? bucketName, string objectName, long offset, long length)
        : base(bucketName, objectName)
    {
        Offset = offset;
        Length = length;
    }
}
