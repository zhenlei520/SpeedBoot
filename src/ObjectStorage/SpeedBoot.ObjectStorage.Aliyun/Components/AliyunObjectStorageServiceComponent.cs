// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

/// <summary>
/// Alibaba Cloud Object Storage Registrar
/// </summary>
public class AliyunObjectStorageServiceComponent : ServiceComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
        => services.AddAliyunStorage();
}
