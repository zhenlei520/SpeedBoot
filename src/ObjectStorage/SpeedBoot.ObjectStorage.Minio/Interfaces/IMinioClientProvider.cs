// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

public interface IMinioClientProvider
{
    MinioObjectStorageOptions MinioObjectStorageOptions { get; }

    /// <summary>
    /// Get Oss Client
    ///
    /// 得到存储客户端
    /// </summary>
    /// <returns></returns>
    MinioClient GetClient();

    /// <summary>
    /// 获得上传凭证
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetUploadCredentialAsync(UploadCredentialRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取下载凭证
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetDownloadCredentialAsync(DownloadCredentialRequest request, CancellationToken cancellationToken = default);
}
