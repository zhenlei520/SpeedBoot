// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun.Internal.Options;

/// <summary>
/// aliyun object storage options
/// 阿里云对象存储配置
///
/// AliyunObjectStorage -> Aliyun: Sts -> Aliyun
/// </summary>
internal class AliyunObjectStorageOptions : Aliyun.AliyunOptions
{
    /// <summary>
    /// ObjectStorage API domain information
    ///
    /// ObjectStorage API域信息
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// The server address of the callback request
    ///
    /// 回调请求的服务器地址
    /// </summary>
    public string CallbackUrl { get; set; } = string.Empty;

    /// <summary>
    /// The value of the request body when the callback is initiated
    ///
    /// 发起回调时请求体的值
    /// </summary>
    public string CallbackBody { get; set; } = "bucket=${bucket}&object=${object}&etag=${etag}&size=${size}&mimeType=${mimeType}";

    /// <summary>
    /// Large files enable resume after power failure
    /// default: true
    ///
    /// 大文件断点续传
    /// 默认值：true
    /// </summary>
    public bool EnableResumableUpload { get; set; } = true;

    /// <summary>
    /// large file length
    /// unit: Byte
    /// default: 10M
    ///
    /// 大文件长度
    /// 单位：字节
    /// 默认：10M
    /// </summary>
    public long BigObjectContentLength { get; set; } = GlobalObjectStorageConfig.BigFileLength;

    /// <summary>
    /// Gets or sets the size of the part (Required when resuming uploads is enabled)
    ///
    /// 获取或设置零件的大小（开启断点续传时需要）
    /// </summary>
    /// <value>The size of the part.</value>
    public long? PartSize { get; set; }

    /// <summary>
    /// Gets or sets the parallel thread count
    /// The parallel thread count
    /// default：3
    ///
    /// 获取或设置并行线程数
    /// 并行线程数
    /// 默认：3
    /// </summary>
    public int ParallelThreadCount { get; set; } = 3;

    /// <summary>
    /// true: quiet mode; false: detail mode
    /// default: true
    ///
    /// true：安静模式； false：详细模式
    /// 默认值：true
    /// </summary>
    public bool Quiet { get; set; } = true;

    /// <summary>
    /// Alibaba Cloud Sts configuration
    /// Used when using sub-users
    ///
    /// 阿里云 Sts 配置
    /// 当使用子用户时使用
    /// </summary>
    public AliyunStsOptions? Sts { get; set; }

    /// <summary>
    /// 存储空间名
    ///
    /// bucket name
    /// </summary>
    public string BucketName { get; set; }
}
