// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

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
        var aliyunObjectStorageSectionName = GetAliyunObjectStorageSectionName();
        var aliyunSectionName = GetAliyunSectionName();

        var aliyunObjectStorageOptions = AppConfiguration.GetOptions<Internal.Options.AliyunObjectStorageOptions>(aliyunObjectStorageSectionName);

        AliyunObjectStorageOptions actualAliyunObjectStorageOptions;
        if (aliyunObjectStorageOptions.Sts != null)
        {
            actualAliyunObjectStorageOptions = new AliyunObjectStorageOptions()
            {
                Sts = aliyunObjectStorageOptions.Sts,
            };
        }
        else if (!aliyunObjectStorageOptions.AccessKeyId.IsNullOrWhiteSpace() &&
                 !aliyunObjectStorageOptions.AccessKeySecret.IsNullOrWhiteSpace())
        {
            actualAliyunObjectStorageOptions = new AliyunObjectStorageOptions()
            {
               Master = new AliyunOptions()
               {
                   AccessKeyId = aliyunObjectStorageOptions.AccessKeyId,
                   AccessKeySecret = aliyunObjectStorageOptions.AccessKeySecret
               }
            };
        }
        else
        {
            var aliyunOptions = AppConfiguration.GetOptions<Internal.Options.AliyunOptions>(aliyunSectionName);
            if (aliyunOptions.Sts != null)
            {
                actualAliyunObjectStorageOptions = new AliyunObjectStorageOptions()
                {
                    Sts = aliyunOptions.Sts,
                };
            }
            else if (!aliyunOptions.AccessKeyId.IsNullOrWhiteSpace() &&
                     !aliyunOptions.AccessKeySecret.IsNullOrWhiteSpace())
            {
                actualAliyunObjectStorageOptions = new AliyunObjectStorageOptions()
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

        actualAliyunObjectStorageOptions.Endpoint = aliyunObjectStorageOptions.Endpoint;
        actualAliyunObjectStorageOptions.CallbackUrl = aliyunObjectStorageOptions.CallbackUrl;
        actualAliyunObjectStorageOptions.CallbackBody = aliyunObjectStorageOptions.CallbackBody;
        actualAliyunObjectStorageOptions.EnableResumableUpload = aliyunObjectStorageOptions.EnableResumableUpload;
        actualAliyunObjectStorageOptions.BigObjectContentLength = aliyunObjectStorageOptions.BigObjectContentLength;
        actualAliyunObjectStorageOptions.PartSize = aliyunObjectStorageOptions.PartSize;
        actualAliyunObjectStorageOptions.Quiet = aliyunObjectStorageOptions.Quiet;
        return actualAliyunObjectStorageOptions;
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
        var objectStorageSectionName = Environment.GetEnvironmentVariable(AliyunStorageConstant.ALIYUN_OBJECT_STORAGE_SECTION_NAME);
        if (objectStorageSectionName.IsNullOrWhiteSpace())
        {
            objectStorageSectionName = AliyunStorageConstant.ALIYUN_OBJECT_STORAGE_SECTION_NAME;
        }
        return objectStorageSectionName;
    }
}
