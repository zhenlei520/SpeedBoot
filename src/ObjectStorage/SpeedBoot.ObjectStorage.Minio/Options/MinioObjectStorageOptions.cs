// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

public class MinioObjectStorageOptions
{
    /// <summary>
    /// account AccessKey ID（optional）
    /// 访问秘钥id（可选）
    /// </summary>
    public string AccessKeyId { get; set; }

    /// <summary>
    /// account AccessKey Secret（optional）
    /// 访问秘钥密码（可选）
    /// </summary>
    public string AccessKeySecret { get; set; }

    /// <summary>
    /// ObjectStorage API domain information
    ///
    /// ObjectStorage API域信息
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// Web Proxy
    ///
    /// Web代理
    /// </summary>
    public IWebProxy? WebProxy { get; set; }

    /// <summary>
    /// enable https
    /// default：true
    ///
    /// 启用https
    /// 默认：true
    /// </summary>
    public bool EnableHttps { get; set; } = true;

    /// <summary>
    /// time out
    ///
    /// 超时时间
    /// </summary>
    public int? Timeout { get; set; }

    /// <summary>
    /// region
    /// default: us-east-1
    ///
    /// 区域
    /// 默认：us-east-1
    /// </summary>
    public string? Region { get; set; }

    /// <summary>
    /// 空间名
    /// </summary>
    public string? BucketName { get; set; }

    /// <summary>
    /// enable file overwrite
    ///
    /// 启用文件覆盖（当为null时使用<paramref>GlobalObjectStorageConfig.EnableOverwrite</paramref>/>）
    /// </summary>
    public bool? EnableOverwrite { get; set; }
}
