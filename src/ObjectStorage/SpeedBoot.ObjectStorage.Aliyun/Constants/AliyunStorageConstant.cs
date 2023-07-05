// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

public static class AliyunStorageConstant
{
    /// <summary>
    /// Aliyun storage configuration node
    ///
    /// 阿里云存储配置节点
    /// </summary>
    public const string ALIYUN_OBJECT_STORAGE_SECTION_NAME = "AliyunObjectStorage";

    /// <summary>
    /// Aliyun configures public nodes
    ///
    /// 阿里云配置公共节点
    /// </summary>
    public const string ALIYUN_SECTION_NAME = "Aliyun";

    /// <summary>
    /// 阿里云临时凭证缓存key
    /// </summary>
    public const string TEMPORARY_CREDENTIALS_CACHE_KEY = "Aliyun.Storage.TemporaryCredentials";
}
