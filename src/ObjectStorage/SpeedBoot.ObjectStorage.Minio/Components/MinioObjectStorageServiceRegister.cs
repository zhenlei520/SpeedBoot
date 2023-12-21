// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio;

/// <summary>
/// Aliyun Object Storage Registrar
/// </summary>
public class MinioObjectStorageServiceRegister : ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
        => services.AddMinio();
}
