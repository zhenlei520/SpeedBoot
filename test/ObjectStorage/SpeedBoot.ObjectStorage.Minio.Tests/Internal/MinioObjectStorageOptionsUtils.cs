﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Minio.Tests;

internal static class MinioObjectStorageOptionsUtils
{
    public static MinioObjectStorageOptions GetMinioObjectStorageOptions(string jsonFile)
    {
        var services = new ServiceCollection();
        services.AddJsonConfiguration(jsonFile);
        services.AddSpeedBoot(options => options.EnabledServiceRegisterComponent = false);
        App.Instance.SetRootServiceProvider(services.BuildServiceProvider());
        return ConfigurationHelper.GetMinioObjectStorageOptions();
    }
}
