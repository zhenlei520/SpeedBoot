// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage;

/// <summary>
/// object info
///
/// 文件信息
/// </summary>
public class ObjectInfoResponse : ObjectStorageResponseBase
{
    /// <summary>
    /// File Stream
    ///
    /// 文件流
    /// </summary>
    public Stream Stream { get; set; }

    /// <summary>
    /// Content Length
    ///
    /// 文件大小
    /// </summary>
    public long ContentLength { get; set; }

    /// <summary>
    /// File Content-Type
    ///
    /// 文件类型
    /// </summary>
    public string ContentType { get; set; }

    /// <summary>
    /// Last Modified
    ///
    /// 最后修改时间
    /// </summary>
    public DateTime LastModified { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the object（Permanently valid is null）
    ///
    /// 到期时间（永久有效为null）
    /// </summary>
    public DateTime? ExpirationTime { get; set; }

    /// <summary>
    /// File md5 value (used to confirm whether it is the same file)
    /// options
    ///
    /// 文件md5值（用于确认是否同一文件）
    /// 可选
    /// </summary>
    public string? ContentMd5 { get; set; }

    /// <summary>
    /// Content Encoding
    /// options
    ///
    /// 编码格式
    /// 可选
    /// </summary>
    public string? ContentEncoding { get; set; }

    /// <summary>
    /// Expand data（not uniform, inconsistent across platforms）
    /// The extended information includes ContentLength, ContentType and other information
    /// Optional
    ///
    /// 扩展信息（不统一，各个平台不一致）
    /// 扩展信息中包含ContentLength、ContentType等信息
    /// 可选
    /// </summary>
    public Dictionary<string, object> Expand { get; set; }
}
