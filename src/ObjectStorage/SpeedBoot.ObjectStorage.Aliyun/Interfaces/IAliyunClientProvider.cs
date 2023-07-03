// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

/// <summary>
/// Get Aliyun Client Provider
/// 阿里云客户端提供者
/// </summary>
public interface IAliyunClientProvider
{
    /// <summary>
    /// aliyun object storage options
    /// 阿里云存储配置
    /// </summary>
    AliyunObjectStorageOptions AliyunObjectStorageOptions { get; }

    /// <summary>
    /// get credentials
    ///
    /// 获取凭证
    /// </summary>
    /// <returns></returns>
    CredentialsResponse GetCredentials();

    /// <summary>
    /// Get Oss Client
    ///
    /// 得到存储客户端
    /// </summary>
    /// <returns></returns>
    IOss GetClient();

    /// <summary>
    /// build object metadata
    ///
    /// 构建对象元数据
    /// </summary>
    /// <returns></returns>
    ObjectMetadata? BuildCallbackMetadata();

    /// <summary>
    /// Whether to enable resuming upload
    ///
    /// 是否启用断点续传
    /// </summary>
    /// <param name="streamSize">file stream size</param>
    /// <returns></returns>
    bool EnableResumableUpload(long streamSize);
}
