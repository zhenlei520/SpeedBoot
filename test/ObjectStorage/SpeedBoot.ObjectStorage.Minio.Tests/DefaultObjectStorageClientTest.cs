// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Minio.Tests;

[TestClass]
public class DefaultObjectStorageClientTest
{
    private const string OBJECT_NAME = "logo.jpg";

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
        var objectStorageClient = GetObjectStorageClient(isUseSts, out MinioObjectStorageOptions minioObjectStorageOptions);
        objectStorageClient.UploadFile(
            minioObjectStorageOptions.BucketName,
            OBJECT_NAME,
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "packageIcon.png"));
    }

    #endregion

    #region Download the file locally（下载文件到本地）

    /// <summary>
    /// Download the file locally
    ///
    /// 下载文件到本地
    /// </summary>
    /// <param name="isUseSts"></param>
    // [DataRow(true)]
    [DataRow(false)]
    [DataTestMethod]
    public void TestDownloadFile(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out MinioObjectStorageOptions minioObjectStorageOptions);
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.jpg");
        objectStorageClient.DownloadFile(minioObjectStorageOptions.BucketName, OBJECT_NAME, filePath);
        Assert.IsTrue(File.Exists(filePath));
    }

    #endregion

    #region Exist（是否存在）

    /// <summary>
    /// Exist（是否存在）
    /// </summary>
    /// <param name="isUseSts"></param>
    // [DataRow(true)]
    [DataRow(false)]
    [DataTestMethod]
    public void Exist(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out MinioObjectStorageOptions minioObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    #endregion

    #region delete file（删除文件）

    /// <summary>
    /// delete file（删除文件）
    /// </summary>
    /// <param name="isUseSts"></param>
    // [DataRow(true)]
    [DataRow(false)]
    [DataTestMethod]
    public void Delete(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out MinioObjectStorageOptions minioObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
        objectStorageClient.Delete(minioObjectStorageOptions.BucketName, OBJECT_NAME);
        Assert.AreEqual(false, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    /// <summary>
    /// delete file（删除文件）
    /// </summary>
    /// <param name="isUseSts"></param>
    // [DataRow(true)]
    [DataRow(false)]
    [DataTestMethod]
    public void BatchDelete(bool isUseSts)
    {
        var objectStorageClient = GetObjectStorageClient(isUseSts, out MinioObjectStorageOptions minioObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
        objectStorageClient.BatchDelete(minioObjectStorageOptions.BucketName, OBJECT_NAME);
        Assert.AreEqual(false, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    #endregion

    #region Get Object Storage Client（获取对象存储客户端）

    private DefaultObjectStorageClient GetObjectStorageClient(
        bool isUseSts,
        out MinioObjectStorageOptions minioObjectStorageOptions)
    {
        minioObjectStorageOptions = isUseSts ? GetMinioObjectStorageOptionsBySts() : GetMinioObjectStorageOptions();
        var minioClientProvider = new DefaultMinioClientProvider(minioObjectStorageOptions);
        return new DefaultObjectStorageClient(minioClientProvider, null);
    }

    #endregion

    #region Get configuration（获取配置）

    /// <summary>
    /// Get configuration (main account)
    ///
    /// 获取配置（主账户）
    /// </summary>
    /// <returns></returns>
    private MinioObjectStorageOptions GetMinioObjectStorageOptions()
    {
        const string file = "minio.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        return ConfigurationHelper.GetMinioObjectStorageOptions();
    }

    /// <summary>
    /// Get configuration (temporary credentials)
    ///
    /// 获取配置（临时凭证）
    /// </summary>
    /// <returns></returns>
    private MinioObjectStorageOptions GetMinioObjectStorageOptionsBySts()
    {
        const string file = "minio.sts.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        return ConfigurationHelper.GetMinioObjectStorageOptions();
    }

    #endregion

}
