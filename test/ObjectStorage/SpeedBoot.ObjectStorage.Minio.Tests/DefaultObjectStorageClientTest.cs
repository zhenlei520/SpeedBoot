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
    [TestMethod]
    public void TestUploadFile()
    {
        var objectStorageClient = GetObjectStorageClient(out var minioObjectStorageOptions);
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
    [TestMethod]
    public void TestDownloadFile()
    {
        var objectStorageClient = GetObjectStorageClient(out MinioObjectStorageOptions minioObjectStorageOptions);
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.jpg");
        objectStorageClient.DownloadFile(minioObjectStorageOptions.BucketName, OBJECT_NAME, filePath);
        Assert.IsTrue(File.Exists(filePath));
    }

    #endregion

    #region Exist（是否存在）

    /// <summary>
    /// Exist（是否存在）
    /// </summary>
    [TestMethod]
    public void Exist()
    {
        var objectStorageClient = GetObjectStorageClient(out var minioObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    #endregion

    #region delete file（删除文件）

    /// <summary>
    /// delete file（删除文件）
    /// </summary>
    [TestMethod]
    public void Delete()
    {
        var objectStorageClient = GetObjectStorageClient(out var minioObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
        objectStorageClient.Delete(minioObjectStorageOptions.BucketName, OBJECT_NAME);
        Assert.AreEqual(false, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    /// <summary>
    /// delete file（删除文件）
    /// </summary>
    [TestMethod]
    public void BatchDelete()
    {
        var objectStorageClient = GetObjectStorageClient(out var minioObjectStorageOptions);

        Assert.AreEqual(true, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
        objectStorageClient.BatchDelete(minioObjectStorageOptions.BucketName, OBJECT_NAME);
        Assert.AreEqual(false, objectStorageClient.Exists(minioObjectStorageOptions.BucketName, OBJECT_NAME));
    }

    #endregion

    #region 凭证

    /// <summary>
    /// 获取上传凭证
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task GetUploadTokenAsync()
    {
        var objectStorageClient = GetObjectStorageClient(out var minioObjectStorageOptions);
        var uploadUrl = objectStorageClient.GetToken(new UploadCredentialRequest(minioObjectStorageOptions.BucketName, OBJECT_NAME)
        {
            Expiry = 24 * 3600
        });
        var httpClient = new HttpClient();
        var fileFullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "packageIcon.png");
        var fileStream = FileUtils.GetBigFileStream(fileFullPath);
        var response = await httpClient.PutAsync(uploadUrl, new StreamContent(fileStream));
        Assert.AreEqual(true, response.IsSuccessStatusCode);
    }

    /// <summary>
    /// 获取下载凭证
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public void GetDownloadToken()
    {
        var objectStorageClient = GetObjectStorageClient(out var minioObjectStorageOptions);
        var token = objectStorageClient.GetToken(new DownloadCredentialRequest(minioObjectStorageOptions.BucketName, OBJECT_NAME)
        {
            Expiry = 60 * 60
        });
        Assert.IsFalse(token.IsNullOrWhiteSpace());
    }

    #endregion

    #region Get Object Storage Client（获取对象存储客户端）

    private DefaultObjectStorageClient GetObjectStorageClient(
        out MinioObjectStorageOptions minioObjectStorageOptions)
    {
        minioObjectStorageOptions = GetMinioObjectStorageOptions();
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
        return MinioObjectStorageOptionsUtils.GetMinioObjectStorageOptions(file);
    }

    #endregion
}
