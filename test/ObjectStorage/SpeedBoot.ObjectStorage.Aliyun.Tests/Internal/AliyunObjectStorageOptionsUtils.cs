// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun.Tests;

internal static class AliyunObjectStorageOptionsUtils
{
    public static AliyunObjectStorageOptions GetAliyunObjectStorageOptions(string file)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        var services = new ServiceCollection();
        services.AddConfiguration(configuration);
        services.AddSpeed(options => options.EnabledServiceRegisterComponent = false);
        return ConfigurationHelper.GetAliyunObjectStorageOptions();
    }
}
