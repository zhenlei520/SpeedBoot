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
    /// <param name="isUseSts"></param>
    [DataRow(true)]
    [DataTestMethod]

    #endregion

    public void TestGetCredentials(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out AliyunObjectStorageOptions aliyunObjectStorageOptions);
        var credentials = objectStorageClient.GetCredentials();
        if (isUseSts)
        {
            Assert.AreEqual(aliyunObjectStorageOptions.AccessKeyId, credentials.AccessKeyId);
            Assert.AreEqual(aliyunObjectStorageOptions.AccessKeySecret, credentials.AccessKeySecret);
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
    /// <param name="isUseSts"></param>
    [DataRow(true)]
    [DataTestMethod]
    public void TestDownloadFile(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out AliyunObjectStorageOptions aliyunObjectStorageOptions);
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.jpg");
        objectStorageClient.DownloadFile(aliyunObjectStorageOptions.BucketName, OBJECT_NAME, filePath);
        Assert.IsTrue(File.Exists(filePath));
    }

    #endregion

    #region Upload file locally（上传文件到云）

    /// <summary>
    /// Upload file locally（上传文件到云）
    /// </summary>
    /// <param name="isUseSts"></param>
    // [DataRow(true)]
    [DataRow(false)]
    [DataTestMethod]
    public void TestUploadFile(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out AliyunObjectStorageOptions aliyunObjectStorageOptions);
        objectStorageClient.UploadFile(
            aliyunObjectStorageOptions.BucketName,
            OBJECT_NAME,
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "packageIcon.png"));
    }

    #endregion

    #region Exist（是否存在）

    /// <summary>
    /// Exist（是否存在）
    /// </summary>
    /// <param name="isUseSts"></param>
    [DataRow(true)]
    [DataTestMethod]
    public void Exist(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out AliyunObjectStorageOptions aliyunObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(aliyunObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    #endregion

    #region delete file（删除文件）
    /// <summary>
    /// delete file（删除文件）
    /// </summary>
    /// <param name="isUseSts"></param>
    [DataRow(true)]
    [DataTestMethod]
    public void Delete(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out AliyunObjectStorageOptions aliyunObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(aliyunObjectStorageOptions.BucketName, OBJECT_NAME));
        objectStorageClient.Delete(aliyunObjectStorageOptions.BucketName, OBJECT_NAME);
        Assert.AreEqual(false, objectStorageClient.Exists(aliyunObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    /// <summary>
    /// delete file（删除文件）
    /// </summary>
    /// <param name="isUseSts"></param>
    [DataRow(true)]
    [DataTestMethod]
    public void BatchDelete(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out AliyunObjectStorageOptions aliyunObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(aliyunObjectStorageOptions.BucketName, OBJECT_NAME));
        objectStorageClient.BatchDelete(aliyunObjectStorageOptions.BucketName, OBJECT_NAME);
        Assert.AreEqual(false, objectStorageClient.Exists(aliyunObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    #endregion

    #region Get Object Storage Client（获取对象存储客户端）

    private DefaultObjectStorageClient GetObjectStorageClient(
        bool isUseSts,
        out AliyunObjectStorageOptions aliyunObjectStorageOptions)
    {
        aliyunObjectStorageOptions = isUseSts ? GetAliyunObjectStorageOptionsByMaster() : GetAliyunObjectStorageOptionsByTemporary();
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
