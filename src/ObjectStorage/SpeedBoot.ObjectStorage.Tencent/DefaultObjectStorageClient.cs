// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using COSXML.Transfer;
using COSXML;
using System;
using System.Collections.Generic;
using System.Text;
using COSXML.Auth;
using COSXML.Common;
using COSXML.Model.Object;
using static COSXML.Model.Tag.ListAllMyBuckets;
using System.Security.Cryptography;

namespace SpeedBoot.ObjectStorage.Tencent
{
    public class DefaultObjectStorageClient : IObjectStorageClient
    {
        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void BatchDelete(BatchDeleteObjectStorageRequest request)
        {
            try
            {
                #region 连接
                
                string secretId = Environment.GetEnvironmentVariable("SECRET_ID"); //用户的 SecretId，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
                string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY"); //用户的 SecretKey，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
                long durationSecond = 600;  //每次请求签名有效时长，单位为秒
                QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
                  secretId, secretKey, durationSecond);
                string appid = Environment.GetEnvironmentVariable("App_Id");//设置腾讯云账户的账户标识 APPID
                string region = Environment.GetEnvironmentVariable("Region"); //设置一个默认的存储桶地域
                CosXmlConfig config = new CosXmlConfig.Builder()
                .IsHttps(true)  //设置默认 HTTPS 请求
                .SetRegion(region)  //设置一个默认的存储桶地域
                .SetDebugLog(true)  //显示日志
                .Build();  //创建 CosXmlConfig 对象

                CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);
                #endregion
                var deleteMultiObjectRequest = new DeleteMultiObjectRequest(request.BucketName);
                //执行请求
                var result = cosXml.DeleteMultiObjects(deleteMultiObjectRequest);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public  Task BatchDeleteAsync(BatchDeleteObjectStorageRequest request, CancellationToken cancellationToken = default)
        {
             BatchDelete(request);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="request"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(DeleteObjectStorageRequest request)
        {
            try
            {
                #region 连接
                string secretId = Environment.GetEnvironmentVariable("SECRET_ID"); //用户的 SecretId，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
                string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY"); //用户的 SecretKey，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
                long durationSecond = 600;  //每次请求签名有效时长，单位为秒
                QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
                  secretId, secretKey, durationSecond);

               
                string appid = Environment.GetEnvironmentVariable("App_Id");//设置腾讯云账户的账户标识 APPID
                string region = Environment.GetEnvironmentVariable("Region"); //设置一个默认的存储桶地域
                CosXmlConfig config = new CosXmlConfig.Builder()
                .IsHttps(true)  //设置默认 HTTPS 请求
                .SetRegion(region)  //设置一个默认的存储桶地域
                .SetDebugLog(true)  //显示日志
                .Build();  //创建 CosXmlConfig 对象

                CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);
                #endregion
                //执行请求
                DeleteObjectRequest request1 = new DeleteObjectRequest(request.BucketName, request.ObjectName);
                var result = cosXml.DeleteObject(request1);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }

        }

        public Task DeleteAsync(DeleteObjectStorageRequest request, CancellationToken cancellationToken = default)
        {
            Delete(request);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Exists(ExistObjectStorageRequest request)
        {
            string secretId = Environment.GetEnvironmentVariable("SECRET_ID"); //用户的 SecretId，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
            string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY"); //用户的 SecretKey，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
            long durationSecond = 600;  //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
              secretId, secretKey, durationSecond);
            string appid = Environment.GetEnvironmentVariable("App_Id");//设置腾讯云账户的账户标识 APPID
            string region = Environment.GetEnvironmentVariable("Region"); //设置一个默认的存储桶地域
            CosXmlConfig config = new CosXmlConfig.Builder()
            .IsHttps(true)  //设置默认 HTTPS 请求
            .SetRegion(region)  //设置一个默认的存储桶地域
            .SetDebugLog(true)  //显示日志
            .Build();  //创建 CosXmlConfig 对象

            DoesObjectExistRequest request1 = new DoesObjectExistRequest(request.BucketName, request.ObjectName);
            CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);
            var test= cosXml.DoesObjectExist(request1);
            return test;
        }

        public Task<bool> ExistsAsync(ExistObjectStorageRequest request, CancellationToken cancellationToken = default)
        {
            var req=Exists(request);
            return Task.FromResult(req);
        }

        /// <summary>
        ///  获取凭证
        /// </summary>
        /// <returns></returns>
        public CredentialsResponse GetCredentials()
        {
            string AccessKeyId = "";
            string AccessKeySecret = "";
            //string SecurityToken = "cc";
            return new CredentialsResponse(AccessKeyId, AccessKeySecret,"");

            //string secretId = Environment.GetEnvironmentVariable("SECRET_ID"); //用户的 SecretId，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
            //string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY"); //用户的 SecretKey，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
            //long durationSecond = 600;  //每次请求签名有效时长，单位为秒
            //QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
            //  secretId, secretKey, durationSecond);


        }

        /// <summary>
        /// 得到文件信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ObjectInfoResponse GetObject(GetObjectInfoRequest request)
        {

            // 存储桶名称，此处填入格式必须为 bucketname-APPID, 其中 APPID 获取参考 https://console.cloud.tencent.com/developer
            //string bucket = "examplebucket-1250000000";
            //string key = "exampleobject"; //对象键
            string secretId = Environment.GetEnvironmentVariable("SECRET_ID"); //用户的 SecretId，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
            string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY"); //用户的 SecretKey，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
            long durationSecond = 600;  //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
              secretId, secretKey, durationSecond);
            string appid = Environment.GetEnvironmentVariable("App_Id");//设置腾讯云账户的账户标识 APPID
            string region = Environment.GetEnvironmentVariable("Region"); //设置一个默认的存储桶地域
            CosXmlConfig config = new CosXmlConfig.Builder()
            .IsHttps(true)  //设置默认 HTTPS 请求
            .SetRegion(region)  //设置一个默认的存储桶地域
            .SetDebugLog(true)  //显示日志
            .Build();  //创建 CosXmlConfig 对象
            DoesObjectExistRequest request1 = new DoesObjectExistRequest(request.BucketName, request.ObjectName);
            CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);




            GetObjectBytesRequest getObjectBytesRequest = new GetObjectBytesRequest(request.BucketName, request.ObjectName);
            //设置进度回调
            getObjectBytesRequest.SetCosProgressCallback(delegate (long completed, long total)
            {
                Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            });
            //执行请求
            GetObjectBytesResult result = cosXml.GetObject(getObjectBytesRequest);
            //获取内容到 byte 数组中
            byte[] content = result.content;
            //请求成功
            Console.WriteLine(result.GetResultInfo());

            return new ObjectInfoResponse()
            {
               
                Stream = new MemoryStream(result.content),
                ContentLength= result.content.Length,
                //Expand = GetExpand()
            };

        }
        /// <summary>
        /// 得到文件信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ObjectInfoResponse GetObject(GetObjectInfoChunkRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 得到文件信息(异步)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ObjectInfoResponse> GetObjectAsync(GetObjectInfoRequest request, CancellationToken cancellationToken = default)
        {
            var req = GetObject(request);
            return Task.FromResult(req);
        }

        /// <summary>
        /// 得到文件信息(异步)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ObjectInfoResponse> GetObjectAsync(GetObjectInfoChunkRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取凭证
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetToken()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Put(PutObjectStorageRequest request)
        {
            try
            {

                string secretId = Environment.GetEnvironmentVariable("SECRET_ID"); //用户的 SecretId，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
                string secretKey = Environment.GetEnvironmentVariable("SECRET_KEY"); //用户的 SecretKey，建议使用子账号密钥，授权遵循最小权限指引，降低使用风险。子账号密钥获取可参见 https://cloud.tencent.com/document/product/598/37140
                long durationSecond = 600;  //每次请求签名有效时长，单位为秒
                QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
                  secretId, secretKey, durationSecond);
                string appid = Environment.GetEnvironmentVariable("App_Id");//设置腾讯云账户的账户标识 APPID
                string region = Environment.GetEnvironmentVariable("Region"); //设置一个默认的存储桶地域
                string fileName = Environment.GetEnvironmentVariable("File_Name"); 
                CosXmlConfig config = new CosXmlConfig.Builder()
                .IsHttps(true)  //设置默认 HTTPS 请求
                .SetRegion(region)  //设置一个默认的存储桶地域
                .SetDebugLog(true)  //显示日志
                .Build();  //创建 CosXmlConfig 对象

                //DoesObjectExistRequest request1 = new DoesObjectExistRequest(request.BucketName, request.ObjectName);
                CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);

                //string srcPath = @"C:\Users\ererj\Desktop\图片";//本地文件绝对路径
                string srcPath = Environment.GetEnvironmentVariable("Src_Path");
                // 打开只读的文件流对象
                FileStream fileStream = new FileStream(srcPath, FileMode.Open, FileAccess.Read);
                // 组装上传请求，其中 offset sendLength 为可选参数
                long offset = 0L;
                long sendLength = fileStream.Length;
                PutObjectRequest putObjectRequest = new PutObjectRequest(request.BucketName, fileName, fileStream, offset, sendLength);

                //设置进度回调
                putObjectRequest.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                PutObjectResult result = cosXml.PutObject(putObjectRequest);
                //关闭文件流
                fileStream.Close();
                //对象的 eTag
                string eTag = result.eTag;
                //对象的 crc64ecma 校验值
                string crc64ecma = result.crc64ecma;
                //打印请求结果
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }


        }



        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task PutAsync(PutObjectStorageRequest request, CancellationToken cancellationToken = default)
        {
            Put(request);
            return Task.CompletedTask;
        }

    }


}
