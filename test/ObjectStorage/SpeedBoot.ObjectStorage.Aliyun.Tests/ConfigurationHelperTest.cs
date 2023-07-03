// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun.Tests;

[TestClass]
public class ConfigurationHelperTest
{
    [TestMethod]
    public void TestGetAliyunObjectStorageOptions()
    {
        var file = "appsettings.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        var aliyunObjectStorageOptions = ConfigurationHelper.GetAliyunObjectStorageOptions();

        Assert.AreEqual(true, aliyunObjectStorageOptions.EnableSts);
        Assert.IsNotNull(aliyunObjectStorageOptions.Sts);
        Assert.AreEqual("AccessKeyId-storage-sts", aliyunObjectStorageOptions.AccessKeyId);
        Assert.AreEqual("AccessKeySecret-storage-sts", aliyunObjectStorageOptions.AccessKeySecret);
        Assert.AreEqual("RegionId-storage-sts", aliyunObjectStorageOptions.Sts.RegionId);
        Assert.AreEqual(900, aliyunObjectStorageOptions.Sts.DurationSeconds);
        Assert.AreEqual("Policy-storage-sts", aliyunObjectStorageOptions.Sts.Policy);
        Assert.AreEqual("RoleArn-storage-sts", aliyunObjectStorageOptions.Sts.RoleArn);
        Assert.AreEqual("RoleSessionName-storage-sts", aliyunObjectStorageOptions.Sts.RoleSessionName);
        Assert.AreEqual(1000, aliyunObjectStorageOptions.Sts.EarlyExpires);
        Assert.AreEqual("Endpoint-storage", aliyunObjectStorageOptions.Endpoint);
        Assert.AreEqual("CallbackUrl-storage", aliyunObjectStorageOptions.CallbackUrl);
        Assert.AreEqual("CallbackBody-storage", aliyunObjectStorageOptions.CallbackBody);
        Assert.AreEqual(true, aliyunObjectStorageOptions.EnableResumableUpload);
        Assert.AreEqual(100, aliyunObjectStorageOptions.BigObjectContentLength);
        Assert.AreEqual(1024, aliyunObjectStorageOptions.PartSize);
        Assert.AreEqual(true, aliyunObjectStorageOptions.Quiet);
        Assert.AreEqual("BucketName-storage", aliyunObjectStorageOptions.BucketName);
    }

    [TestMethod]
    public void TestGetAliyunObjectStorageOptions2()
    {
        var file = "appsettings2.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        var aliyunObjectStorageOptions = ConfigurationHelper.GetAliyunObjectStorageOptions();

        Assert.AreEqual(false, aliyunObjectStorageOptions.EnableSts);
        Assert.IsNull(aliyunObjectStorageOptions.Sts);
        Assert.AreEqual("AccessKeyId-storage-master-2", aliyunObjectStorageOptions.AccessKeyId);
        Assert.AreEqual("AccessKeySecret-storage-master-2", aliyunObjectStorageOptions.AccessKeySecret);
        Assert.AreEqual("Endpoint-storage-2", aliyunObjectStorageOptions.Endpoint);
        Assert.AreEqual("CallbackUrl-storage-2", aliyunObjectStorageOptions.CallbackUrl);
        Assert.AreEqual("CallbackBody-storage-2", aliyunObjectStorageOptions.CallbackBody);
        Assert.AreEqual(true, aliyunObjectStorageOptions.EnableResumableUpload);
        Assert.AreEqual(1002, aliyunObjectStorageOptions.BigObjectContentLength);
        Assert.AreEqual(10242, aliyunObjectStorageOptions.PartSize);
        Assert.AreEqual(true, aliyunObjectStorageOptions.Quiet);
        Assert.AreEqual("BucketName-storage-2", aliyunObjectStorageOptions.BucketName);
    }

    [TestMethod]
    public void TestGetAliyunObjectStorageOptions3()
    {
        var file = "appsettings3.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        var aliyunObjectStorageOptions = ConfigurationHelper.GetAliyunObjectStorageOptions();

        Assert.AreEqual(true, aliyunObjectStorageOptions.EnableSts);
        Assert.IsNotNull(aliyunObjectStorageOptions.Sts);
        Assert.AreEqual("AccessKeyId-sts", aliyunObjectStorageOptions.AccessKeyId);
        Assert.AreEqual("AccessKeySecret-sts", aliyunObjectStorageOptions.AccessKeySecret);
        Assert.AreEqual("RegionId-sts", aliyunObjectStorageOptions.Sts.RegionId);
        Assert.AreEqual(903, aliyunObjectStorageOptions.Sts.DurationSeconds);
        Assert.AreEqual("Policy-sts", aliyunObjectStorageOptions.Sts.Policy);
        Assert.AreEqual("RoleArn-sts", aliyunObjectStorageOptions.Sts.RoleArn);
        Assert.AreEqual("RoleSessionName-sts", aliyunObjectStorageOptions.Sts.RoleSessionName);
        Assert.AreEqual(10003, aliyunObjectStorageOptions.Sts.EarlyExpires);
        Assert.AreEqual("Endpoint-storage-3", aliyunObjectStorageOptions.Endpoint);
        Assert.AreEqual("CallbackUrl-storage-3", aliyunObjectStorageOptions.CallbackUrl);
        Assert.AreEqual("CallbackBody-storage-3", aliyunObjectStorageOptions.CallbackBody);
        Assert.AreEqual(true, aliyunObjectStorageOptions.EnableResumableUpload);
        Assert.AreEqual(1003, aliyunObjectStorageOptions.BigObjectContentLength);
        Assert.AreEqual(10243, aliyunObjectStorageOptions.PartSize);
        Assert.AreEqual(true, aliyunObjectStorageOptions.Quiet);
        Assert.AreEqual("BucketName-storage-3", aliyunObjectStorageOptions.BucketName);
    }

    [TestMethod]
    public void TestGetAliyunObjectStorageOptions4()
    {
        var file = "appsettings4.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        var aliyunObjectStorageOptions = ConfigurationHelper.GetAliyunObjectStorageOptions();

        Assert.AreEqual(false, aliyunObjectStorageOptions.EnableSts);
        Assert.IsNull(aliyunObjectStorageOptions.Sts);
        Assert.AreEqual("AccessKeyId-master", aliyunObjectStorageOptions.AccessKeyId);
        Assert.AreEqual("AccessKeySecret-master", aliyunObjectStorageOptions.AccessKeySecret);
        Assert.AreEqual("Endpoint-storage-4", aliyunObjectStorageOptions.Endpoint);
        Assert.AreEqual("CallbackUrl-storage-4", aliyunObjectStorageOptions.CallbackUrl);
        Assert.AreEqual("CallbackBody-storage-4", aliyunObjectStorageOptions.CallbackBody);
        Assert.AreEqual(false, aliyunObjectStorageOptions.EnableResumableUpload);
        Assert.AreEqual(1004, aliyunObjectStorageOptions.BigObjectContentLength);
        Assert.AreEqual(10244, aliyunObjectStorageOptions.PartSize);
        Assert.AreEqual(false, aliyunObjectStorageOptions.Quiet);
        Assert.AreEqual("BucketName-storage-4", aliyunObjectStorageOptions.BucketName);
    }
}
