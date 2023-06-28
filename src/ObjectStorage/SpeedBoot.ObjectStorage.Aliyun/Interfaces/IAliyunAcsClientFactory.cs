// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

/// <summary>
/// Acs客户端工厂
/// </summary>
public interface IAliyunAcsClientFactory
{
    IAcsClient GetAcsClient(string accessKeyId, string accessKeySecret, string regionId);
}
