// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun.Tests;

internal static class AliyunObjectStorageOptionsUtils
{
    public static AliyunObjectStorageOptions GetAliyunObjectStorageOptions(string jsonFile)
    {
        var services = new ServiceCollection();
        services.AddJsonConfiguration(jsonFile);
        services.AddSpeed(options => options.EnabledServiceRegisterComponent = false);
        App.ApplicationExternal.SetServiceProvider(services.BuildServiceProvider());
        return ConfigurationHelper.GetAliyunObjectStorageOptions();
    }
}
