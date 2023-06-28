// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

internal static class ConfigurationHelper
{
    private static AliyunObjectStorageOptions? _aliyunObjectStorageOptions;
    /// <summary>
    /// Get the configuration information of Aliyun storage used
    ///
    /// 得到使用的阿里云存储配置信息
    /// </summary>
    /// <returns></returns>
    public static AliyunObjectStorageOptions GetAliyunObjectStorageOptions()
    {
        if (_aliyunObjectStorageOptions != null)
            return _aliyunObjectStorageOptions;

        var aliyunObjectStorageSectionName = GetAliyunObjectStorageSectionName();

        var aliyunObjectStorageOptions = AppConfiguration.GetOptions<Internal.Options.AliyunObjectStorageOptions>(aliyunObjectStorageSectionName);

        if (aliyunObjectStorageOptions.Sts != null)
        {
            _aliyunObjectStorageOptions = new AliyunObjectStorageOptions()
            {
                Sts = aliyunObjectStorageOptions.Sts,
            };
        }
        else if (!aliyunObjectStorageOptions.AccessKeyId.IsNullOrWhiteSpace() &&
                 !aliyunObjectStorageOptions.AccessKeySecret.IsNullOrWhiteSpace())
        {
            _aliyunObjectStorageOptions = new AliyunObjectStorageOptions()
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
            var aliyunSectionName = GetAliyunSectionName();
            var aliyunOptions = AppConfiguration.GetOptions<Internal.Options.AliyunOptions>(aliyunSectionName);
            if (aliyunOptions.Sts != null)
            {
                _aliyunObjectStorageOptions = new AliyunObjectStorageOptions()
                {
                    Sts = aliyunOptions.Sts,
                };
            }
            else if (!aliyunOptions.AccessKeyId.IsNullOrWhiteSpace() &&
                     !aliyunOptions.AccessKeySecret.IsNullOrWhiteSpace())
            {
                _aliyunObjectStorageOptions = new AliyunObjectStorageOptions()
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

        _aliyunObjectStorageOptions.Endpoint = aliyunObjectStorageOptions.Endpoint;
        _aliyunObjectStorageOptions.CallbackUrl = aliyunObjectStorageOptions.CallbackUrl;
        _aliyunObjectStorageOptions.CallbackBody = aliyunObjectStorageOptions.CallbackBody;
        _aliyunObjectStorageOptions.EnableResumableUpload = aliyunObjectStorageOptions.EnableResumableUpload;
        _aliyunObjectStorageOptions.BigObjectContentLength = aliyunObjectStorageOptions.BigObjectContentLength;
        _aliyunObjectStorageOptions.PartSize = aliyunObjectStorageOptions.PartSize;
        _aliyunObjectStorageOptions.Quiet = aliyunObjectStorageOptions.Quiet;
        return _aliyunObjectStorageOptions;
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
