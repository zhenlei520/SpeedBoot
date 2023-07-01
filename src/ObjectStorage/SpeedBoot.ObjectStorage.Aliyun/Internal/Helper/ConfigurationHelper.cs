﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: InternalsVisibleTo("SpeedBoot.ObjectStorage.Aliyun.Tests")]

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

internal static class ConfigurationHelper
{
    /// <summary>
    /// Get the configuration information of Aliyun storage used
    ///
    /// 得到使用的阿里云存储配置信息
    /// </summary>
    /// <returns></returns>
    public static AliyunObjectStorageOptions GetAliyunObjectStorageOptions()
    {
        AliyunObjectStorageOptions? aliyunObjectStorageOptionsActual;
        var aliyunObjectStorageSectionName = GetAliyunObjectStorageSectionName();

        var aliyunObjectStorageOptions = AppConfiguration.GetOptions<Internal.Options.AliyunObjectStorageOptions>(aliyunObjectStorageSectionName);

        if (aliyunObjectStorageOptions.Sts != null)
        {
            aliyunObjectStorageOptionsActual = new AliyunObjectStorageOptions()
            {
                Sts = aliyunObjectStorageOptions.Sts,
            };
        }
        else if (!aliyunObjectStorageOptions.AccessKeyId.IsNullOrWhiteSpace() &&
                 !aliyunObjectStorageOptions.AccessKeySecret.IsNullOrWhiteSpace())
        {
            aliyunObjectStorageOptionsActual = new AliyunObjectStorageOptions()
            {
                Master = new AliyunOptions
                {
                    AccessKeyId = aliyunObjectStorageOptions.AccessKeyId,
                    AccessKeySecret = aliyunObjectStorageOptions.AccessKeySecret
                }
            };
        }
        else
        {
            var aliyunSectionName = GetAliyunSectionName();
            var aliyunOptions = AppConfiguration.GetOptions<Internal.Options.AliyunOptions>(aliyunSectionName);
            if (aliyunOptions.Sts != null)
            {
                aliyunObjectStorageOptionsActual = new AliyunObjectStorageOptions()
                {
                    Sts = aliyunOptions.Sts,
                };
            }
            else if (!aliyunOptions.AccessKeyId.IsNullOrWhiteSpace() &&
                     !aliyunOptions.AccessKeySecret.IsNullOrWhiteSpace())
            {
                aliyunObjectStorageOptionsActual = new AliyunObjectStorageOptions()
                {
                    Master = new AliyunOptions
                    {
                        AccessKeyId = aliyunOptions.AccessKeyId,
                        AccessKeySecret = aliyunOptions.AccessKeySecret
                    }
                };
            }
            else
            {
                throw new SpeedException("Aliyun storage configuration is indeed");
            }
        }

        aliyunObjectStorageOptionsActual.Endpoint = aliyunObjectStorageOptions.Endpoint;
        aliyunObjectStorageOptionsActual.CallbackUrl = aliyunObjectStorageOptions.CallbackUrl;
        aliyunObjectStorageOptionsActual.CallbackBody = aliyunObjectStorageOptions.CallbackBody;
        aliyunObjectStorageOptionsActual.EnableResumableUpload = aliyunObjectStorageOptions.EnableResumableUpload;
        aliyunObjectStorageOptionsActual.BigObjectContentLength = aliyunObjectStorageOptions.BigObjectContentLength;
        aliyunObjectStorageOptionsActual.PartSize = aliyunObjectStorageOptions.PartSize;
        aliyunObjectStorageOptionsActual.Quiet = aliyunObjectStorageOptions.Quiet;
        aliyunObjectStorageOptionsActual.BucketName = aliyunObjectStorageOptions.BucketName;
        return aliyunObjectStorageOptionsActual;
    }

    /// <summary>
    /// Get the Aliyun storage configuration node name
    ///
    /// 得到阿里云存储配置节点名
    /// </summary>
    /// <returns></returns>
    private static string GetAliyunObjectStorageSectionName()
    {
        var objectStorageSectionName = Environment.GetEnvironmentVariable(AliyunStorageConstant.ALIYUN_OBJECT_STORAGE_SECTION_NAME);
        if (objectStorageSectionName.IsNullOrWhiteSpace())
        {
            objectStorageSectionName = AliyunStorageConstant.ALIYUN_OBJECT_STORAGE_SECTION_NAME;
        }
        return objectStorageSectionName;
    }

    /// <summary>
    /// Get the Aliyun public configuration node name
    ///
    /// 得到阿里云公共配置节点名
    /// </summary>
    /// <returns></returns>
    private static string GetAliyunSectionName()
    {
        var objectStorageSectionName = Environment.GetEnvironmentVariable(AliyunStorageConstant.ALIYUN_SECTION_NAME);
        if (objectStorageSectionName.IsNullOrWhiteSpace())
        {
            objectStorageSectionName = AliyunStorageConstant.ALIYUN_SECTION_NAME;
        }
        return objectStorageSectionName;
    }
}
