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

    #region Get Object Storage Client（获取对象存储客户端）

    private DefaultObjectStorageClient GetObjectStorageClient(
        bool isUseSts,
        out MinioObjectStorageOptions minioObjectStorageOptions)
    {
        minioObjectStorageOptions = isUseSts ? GetMinioObjectStorageOptions() : GetAliyunObjectStorageOptionsBySts();
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
        var file = "minio.json";
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
    private MinioObjectStorageOptions GetAliyunObjectStorageOptionsBySts()
    {
        var file = "minio.sts.json";
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file)
            .Build();
        AppCore.ConfigureConfiguration(configuration);
        return ConfigurationHelper.GetMinioObjectStorageOptions();
    }

    #endregion
}
