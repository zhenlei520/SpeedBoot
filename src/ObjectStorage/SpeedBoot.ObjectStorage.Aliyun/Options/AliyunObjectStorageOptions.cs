// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

/// <summary>
/// aliyun object storage
///
/// 阿里云存储配置
/// </summary>
public class AliyunObjectStorageOptions
{

    #region Aliyun account information 账户信息

    /// <summary>
    /// Aliyun master account information
    ///
    /// 阿里云主账户信息
    /// </summary>
    public AliyunOptions? Master { get; set; }

    /// <summary>
    /// Aliyun temporary credential information
    ///
    /// 阿里云临时凭证信息
    /// </summary>
    public AliyunStsOptions? Sts { get; set; }

    #endregion

    /// <summary>
    /// If Sts is not equal to null, use temporary credentials
    ///
    /// 如果Sts不等于null，则使用临时凭证
    /// </summary>
    public bool EnableSts => Sts != null;

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
    /// true: quiet mode; false: detail mode
    /// default: true
    ///
    /// true：安静模式； false：详细模式
    /// 默认值：true
    /// </summary>
    public bool Quiet { get; set; } = true;

    /// <summary>
    /// 存储空间名
    ///
    /// bucket name
    /// </summary>
    public string BucketName { get; set; }

    /// <summary>
    /// enable file overwrite
    ///
    /// 启用文件覆盖（当为null时使用<paramref>GlobalObjectStorageConfig.EnableOverwrite</paramref>/>）
    /// </summary>
    public bool? EnableOverwrite { get; set; }

    /// <summary>
    /// cache configuration
    ///
    /// 缓存配置
    /// </summary>
    public MemoryCacheOptions? MemoryCacheOptions { get; set; }
}
