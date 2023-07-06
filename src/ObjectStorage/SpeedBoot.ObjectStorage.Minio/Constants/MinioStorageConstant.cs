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
    public const string Minio_OBJECT_STORAGE_SECTION_NAME = "MinioObjectStorage";

    /// <summary>
    /// Minio临时凭证缓存key
    /// </summary>
    public const string TEMPORARY_CREDENTIALS_CACHE_KEY = "Minio.Storage.TemporaryCredentials";
}
