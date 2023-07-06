// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

public interface IMinioClientProvider
{
    MinioObjectStorageOptions MinioObjectStorageOptions { get; }

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
    MinioClient GetClient();
}
