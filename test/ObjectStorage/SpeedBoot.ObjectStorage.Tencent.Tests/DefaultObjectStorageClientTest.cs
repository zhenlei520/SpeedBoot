// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SpeedBoot.ObjectStorage.Tencent.Tests;

[TestClass]
public class DefaultObjectStorageClientTest
{

    [TestMethod]
    public void Delete()
    {
        DefaultObjectStorageClient defaultObjectStorageClient = new DefaultObjectStorageClient();
        DeleteObjectStorageRequest deleteObjectStorageRequest = new DeleteObjectStorageRequest()
        {
            BucketName = "",//桶名
            ObjectName = "OAuth 1.0a.jpg"
        };
        Environment.SetEnvironmentVariable("SECRET_ID", "");
        Environment.SetEnvironmentVariable("SECRET_KEY", "");

        Environment.SetEnvironmentVariable("App_Id", "");
        Environment.SetEnvironmentVariable("Region", "");//地域 例如：ap-guangzhou

        defaultObjectStorageClient.Delete(deleteObjectStorageRequest);
    }

    [TestMethod]
    public async Task DeleteAsync()
    {
        DefaultObjectStorageClient defaultObjectStorageClient = new DefaultObjectStorageClient();
        DeleteObjectStorageRequest deleteObjectStorageRequest = new DeleteObjectStorageRequest()
        {
            BucketName = "",
            ObjectName = "OAuth 1.0.jpg"
        };
        Environment.SetEnvironmentVariable("SECRET_ID", "");
        Environment.SetEnvironmentVariable("SECRET_KEY", "");

        Environment.SetEnvironmentVariable("App_Id", "");
        Environment.SetEnvironmentVariable("Region", "");


        await defaultObjectStorageClient.DeleteAsync(deleteObjectStorageRequest);
    }

    [TestMethod]
    public void Exists()
    {
        Environment.SetEnvironmentVariable("SECRET_ID", "");
        Environment.SetEnvironmentVariable("SECRET_KEY", "");

        Environment.SetEnvironmentVariable("App_Id", "");
        Environment.SetEnvironmentVariable("Region", "");
        DefaultObjectStorageClient defaultObjectStorageClient = new DefaultObjectStorageClient();
        ExistObjectStorageRequest existObjectStorageRequest = new ExistObjectStorageRequest()
        {
            BucketName = "",
            ObjectName = "OAuth 1.0a.jpg"
        };
        defaultObjectStorageClient.Exists(existObjectStorageRequest);
    }
   
   
    [TestMethod]
    public void Put()
    {
        Environment.SetEnvironmentVariable("SECRET_ID", "");
        Environment.SetEnvironmentVariable("SECRET_KEY", "");
        Environment.SetEnvironmentVariable("App_Id", "");
        Environment.SetEnvironmentVariable("Region", "");
        Environment.SetEnvironmentVariable("File_Name", "");//图片名称
        Environment.SetEnvironmentVariable("Src_Path", @"C:\Desktop\图片\u=3853345508,384760633&fm=253&fmt=auto&app=120&f=JPEG.webp");//本地路径
        DefaultObjectStorageClient defaultObjectStorageClient = new DefaultObjectStorageClient();
        PutObjectStorageRequest putObjectStorageRequest = new PutObjectStorageRequest()
        {
            BucketName = "",
            ObjectName = ""//桶内文件名
        };
        defaultObjectStorageClient.Put(putObjectStorageRequest);

    }

    [TestMethod]
    public void PutAsync()
    {
        Environment.SetEnvironmentVariable("SECRET_ID", "");
        Environment.SetEnvironmentVariable("SECRET_KEY", "");
        Environment.SetEnvironmentVariable("App_Id", "");
        Environment.SetEnvironmentVariable("Region", "");
        Environment.SetEnvironmentVariable("File_Name", "");
        Environment.SetEnvironmentVariable("Src_Path", @"C:\Users\u=3853345508,384760633&fm=253&fmt=auto&app=120&f=JPEG.webp");//本地路径
        DefaultObjectStorageClient defaultObjectStorageClient = new DefaultObjectStorageClient();
        PutObjectStorageRequest putObjectStorageRequest = new PutObjectStorageRequest()
        {
            BucketName = "",
            ObjectName = "u=3853345508,384760633&fm=253&fmt=auto&app=120&f=JPEG.webp"
        };
        defaultObjectStorageClient.PutAsync(putObjectStorageRequest);

    }

    /// <summary>
    /// 得到文件信息
    /// </summary>
    [TestMethod]
    public void GetObject()
    {
        Environment.SetEnvironmentVariable("SECRET_ID", "");
        Environment.SetEnvironmentVariable("SECRET_KEY", "");
        Environment.SetEnvironmentVariable("App_Id", "");
        Environment.SetEnvironmentVariable("Region", "");
        GetObjectInfoRequest getObjectInfoRequest = new GetObjectInfoRequest()
        {
            BucketName = "",
            ObjectName = "05.jpg"
        };
        DefaultObjectStorageClient defaultObjectStorageClient = new DefaultObjectStorageClient();
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "05.jpg");
        defaultObjectStorageClient.DownloadFile(getObjectInfoRequest.BucketName, getObjectInfoRequest.ObjectName, filePath);
        Assert.IsTrue(File.Exists(filePath));
        //defaultObjectStorageClient.GetObject(getObjectInfoRequest);
    }
}
