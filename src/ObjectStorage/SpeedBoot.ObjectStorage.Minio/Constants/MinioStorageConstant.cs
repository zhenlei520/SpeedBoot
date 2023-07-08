// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

public static class MinioStorageConstant
{
    /// <summary>
    /// Minio storage configuration node
    ///
    /// Minio存储配置节点
    /// </summary>
    public const string MINIO_OBJECT_STORAGE_SECTION_NAME = "MinioObjectStorage";

    /// <summary>
    /// Minio临时凭证缓存key
    /// </summary>
    public const string TEMPORARY_CREDENTIALS_CACHE_KEY = "Minio.Storage.TemporaryCredentials";

    /// <summary>
    /// 默认上传文件失效时间
    /// </summary>
    public const int DEFAULT_PUT_OBJECT_EXPIRATION_TIME = 10 * 60;

    /// <summary>
    /// 默认最大上传文件失效时间
    /// </summary>
    public const int DEFAULT_PUT_OBJECT_MAX_EXPIRATION_TIME = 7 * 24 * 3600;
}
