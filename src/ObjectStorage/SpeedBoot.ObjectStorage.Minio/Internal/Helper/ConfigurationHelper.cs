// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("SpeedBoot.ObjectStorage.Minio.Tests")]

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

internal static class ConfigurationHelper
{
    /// <summary>
    /// Get the configuration information of Aliyun storage used
    ///
    /// 得到使用的Minio存储配置信息
    /// </summary>
    /// <returns></returns>
    public static MinioObjectStorageOptions GetMinioObjectStorageOptions()
    {
        var minioObjectStorageSectionName = GetMinioObjectStorageSectionName();

        var minioObjectStorageOptions = App.ApplicationExternal.GetOptions<SpeedBoot.ObjectStorage.Minio.Internal.Options.MinioObjectStorageOptions>(minioObjectStorageSectionName);

        return new MinioObjectStorageOptions()
        {
            AccessKeyId = minioObjectStorageOptions.AccessKeyId,
            AccessKeySecret = minioObjectStorageOptions.AccessKeySecret,
            Endpoint = minioObjectStorageOptions.Endpoint,
            EnableHttps = minioObjectStorageOptions.EnableHttps,
            Timeout = minioObjectStorageOptions.Timeout,
            Region = minioObjectStorageOptions.Region,
            BucketName = minioObjectStorageOptions.BucketName,
            EnableOverwrite=minioObjectStorageOptions.EnableOverwrite
        };
    }

    /// <summary>
    /// Get the Minio storage configuration node name
    ///
    /// 得到Minio存储配置节点名
    /// </summary>
    /// <returns></returns>
    private static string GetMinioObjectStorageSectionName()
    {
        var objectStorageSectionName = Environment.GetEnvironmentVariable(MinioStorageConstant.MINIO_OBJECT_STORAGE_SECTION_NAME);
        if (objectStorageSectionName.IsNullOrWhiteSpace())
        {
            objectStorageSectionName = MinioStorageConstant.MINIO_OBJECT_STORAGE_SECTION_NAME;
        }
        return objectStorageSectionName;
    }
}
