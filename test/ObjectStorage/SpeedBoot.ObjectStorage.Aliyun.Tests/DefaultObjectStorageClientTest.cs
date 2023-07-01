// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun.Tests;

[TestClass]
public class DefaultObjectStorageClientTest
{
    private const string OBJECT_NAME = "logo.jpg";

    #region Get Credentials（Get Credentials）

    /// <summary>
    /// Get Credentials
    ///
    /// 获取凭证
    /// </summary>
    /// <param name="isMaster"></param>
    [DataRow(true)]
    [DataTestMethod]

    #endregion

    public void TestGetCredentials(bool isMaster)
    {
        var objectStorageClient = GetObjectStorageClient(isMaster, out AliyunObjectStorageOptions aliyunObjectStorageOptions);
        var credentials = objectStorageClient.GetCredentials();
        Assert.IsNotNull(aliyunObjectStorageOptions.Master);
        if (isMaster)
        {
            Assert.AreEqual(aliyunObjectStorageOptions.Master.AccessKeyId, credentials.AccessKeyId);
            Assert.AreEqual(aliyunObjectStorageOptions.Master.AccessKeySecret, credentials.AccessKeySecret);
            Assert.AreEqual(null, credentials.SecurityToken);
        }
        else
        {
            Assert.IsNotNull(credentials.AccessKeyId);
            Assert.IsNotNull(credentials.AccessKeySecret);
            Assert.IsNotNull(credentials.SecurityToken);
        }
    }

    #region Download the file locally（下载文件到本地）

    /// <summary>
    /// Download the file locally
    ///
    /// 下载文件到本地
    /// </summary>
    /// <param name="isMaster"></param>
    [DataRow(true)]
    [DataTestMethod]
    public void TestDownloadFile(bool isMaster)
    {
        var objectStorageClient = GetObjectStorageClient(isMaster, out AliyunObjectStorageOptions aliyunObjectStorageOptions);
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.jpg");
        objectStorageClient.DownloadFile(aliyunObjectStorageOptions.BucketName, OBJECT_NAME, filePath);
        Assert.IsTrue(File.Exists(filePath));
    }

    #endregion

    #region Get Object Storage Client（获取对象存储客户端）

    private DefaultObjectStorageClient GetObjectStorageClient(
        bool isMaster,
        out AliyunObjectStorageOptions aliyunObjectStorageOptions)
    {
        aliyunObjectStorageOptions = isMaster ? GetAliyunObjectStorageOptionsByMaster() : GetAliyunObjectStorageOptionsByTemporary();
        var aliyunClientProvider = new DefaultAliyunClientProvider(aliyunObjectStorageOptions);
        return new DefaultObjectStorageClient(aliyunClientProvider, null);
    }

    #endregion

    #region Get configuration（获取配置）

    /// <summary>
    /// Get configuration (main account)
    ///
    /// 获取配置（主账户）
    /// </summary>
    /// <returns></returns>
    private AliyunObjectStorageOptions GetAliyunObjectStorageOptionsByMaster()
    {
        var file = "aliyun.master.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        return ConfigurationHelper.GetAliyunObjectStorageOptions();
    }

    /// <summary>
    /// Get configuration (temporary credentials)
    ///
    /// 获取配置（临时凭证）
    /// </summary>
    /// <returns></returns>
    private AliyunObjectStorageOptions GetAliyunObjectStorageOptionsByTemporary()
    {
        var file = "aliyun.temporary.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        return ConfigurationHelper.GetAliyunObjectStorageOptions();
    }

    #endregion
}
