// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun;

/// <summary>
/// Default Aliyun provider
///
/// 默认阿里云提供者
/// </summary>
public class DefaultAliyunClientProvider : IAliyunClientProvider
{
    private readonly IAliyunAcsClientFactory _aliyunAcsClientFactory;
    private readonly IMemoryCache _memoryCache;

    public AliyunObjectStorageOptions AliyunObjectStorageOptions { get; }

    public DefaultAliyunClientProvider(
        AliyunObjectStorageOptions aliyunObjectStorageOptions,
        IAliyunAcsClientFactory? aliyunAcsClientFactory = null,
        IMemoryCache? memoryCache = null)
    {
        AliyunObjectStorageOptions = aliyunObjectStorageOptions;
        _aliyunAcsClientFactory = aliyunAcsClientFactory ?? new DefaultAliyunAcsClientFactory();
        _memoryCache = memoryCache ?? (aliyunObjectStorageOptions.MemoryCacheOptions != null ?
            new MemoryCache(aliyunObjectStorageOptions.MemoryCacheOptions) :
            new MemoryCache(new MemoryCacheOptions()));
    }

    #region get oss storage certificate（得到凭证或临时凭证）

    /// <summary>
    /// get oss storage certificate or temporary certificate
    ///
    /// 得到凭证或临时凭证
    /// </summary>
    /// <returns></returns>
    public CredentialsResponse GetCredentials()
    {
        if (!AliyunObjectStorageOptions.EnableSts)
        {
            return new CredentialsResponse(
                AliyunObjectStorageOptions.AccessKeyId,
                AliyunObjectStorageOptions.AccessKeySecret,
                null);
        }

        var securityToken = GetTemporaryCredentials();
        return new CredentialsResponse(securityToken.AccessKeyId, securityToken.AccessKeySecret, securityToken.SecurityToken);
    }

    #endregion

    #region Get Aliyun Oss Client（得到阿里云Oss客户端）

    /// <summary>
    /// Get Aliyun Oss Client（得到阿里云Oss客户端）
    /// </summary>
    /// <returns></returns>
    public IOss GetClient()
    {
        var credential = GetCredentials();
        return new OssClient(
            AliyunObjectStorageOptions.Endpoint,
            credential.AccessKeyId,
            credential.AccessKeySecret,
            credential.SecurityToken);
    }

    #region get security credentials（得到安全凭证）

    /// <summary>
    /// get security credentials
    ///
    /// 得到安全凭证
    /// </summary>
    /// <returns></returns>
    private TemporaryCredentialsResponse GetTemporaryCredentials()
    {
        if (_memoryCache.TryGetValue(
                AliyunStorageConstant.TEMPORARY_CREDENTIALS_CACHE_KEY,
                out TemporaryCredentialsResponse? temporaryCredentials))
            return temporaryCredentials!;

        temporaryCredentials = GetTemporaryCredentials(
            AliyunObjectStorageOptions.Sts!.RegionId!,
            AliyunObjectStorageOptions.AccessKeyId,
            AliyunObjectStorageOptions.AccessKeySecret,
            AliyunObjectStorageOptions.Sts.RoleArn,
            AliyunObjectStorageOptions.Sts.RoleSessionName,
            AliyunObjectStorageOptions.Sts.Policy,
            AliyunObjectStorageOptions.Sts.DurationSeconds);
        SetTemporaryCredentials(temporaryCredentials);
        return temporaryCredentials!;
    }

    private TemporaryCredentialsResponse GetTemporaryCredentials(
        string regionId,
        string accessKeyId,
        string accessKeySecret,
        string roleArn,
        string roleSessionName,
        string policy,
        long durationSeconds)
    {
        var client = _aliyunAcsClientFactory.GetAcsClient(accessKeyId, accessKeySecret, regionId);
        var request = new AssumeRoleRequest
        {
            ContentType = AliyunFormatType.JSON,
            RoleArn = roleArn,
            RoleSessionName = roleSessionName,
            DurationSeconds = durationSeconds
        };
        if (!string.IsNullOrEmpty(policy))
            request.Policy = policy;
        var response = client.GetAcsResponse(request);
        // if (response.HttpResponse.isSuccess()) //todo: Get Sts response information is null, waiting for repair: https://github.com/aliyun/aliyun-openapi-net-sdk/pull/401
        // {
        return new TemporaryCredentialsResponse(
            response.Credentials.AccessKeyId,
            response.Credentials.AccessKeySecret,
            response.Credentials.SecurityToken)
        {
            Expiration = DateTime.Parse(response.Credentials.Expiration)
        };
        // }

        // string responseContent = Encoding.Default.GetString(response.HttpResponse.Content);
        // string message =
        //     $"Aliyun.Client: Failed to obtain temporary credentials, RequestId: {response.RequestId}, Status: {response.HttpResponse.Status}, Message: {responseContent}";
        // _logger?.LogWarning(
        //     "Aliyun.Client: Failed to obtain temporary credentials, RequestId: {RequestId}, Status: {Status}, Message: {Message}",
        //     response.RequestId, response.HttpResponse.Status, responseContent);
        //
        // throw new Exception(message);
    }

    /// <summary>
    /// set temporary credentials
    /// </summary>
    /// <param name="credentials"></param>
    private void SetTemporaryCredentials(TemporaryCredentialsResponse credentials)
    {
        var timespan = (DateTime.UtcNow - credentials.Expiration!.Value).TotalSeconds - AliyunObjectStorageOptions.Sts!.EarlyExpires;
        if (timespan >= 0)
            _memoryCache.Set(AliyunStorageConstant.TEMPORARY_CREDENTIALS_CACHE_KEY, credentials, TimeSpan.FromSeconds(timespan));
    }

    #endregion

    #endregion

    #region Build object metadata（构建对象元数据）

    /// <summary>
    /// Build object metadata
    ///
    /// 构建对象元数据
    /// </summary>
    /// <returns></returns>
    public ObjectMetadata? BuildCallbackMetadata()
    {
        if (AliyunObjectStorageOptions.CallbackBody.IsNullOrWhiteSpace() ||
            AliyunObjectStorageOptions.CallbackUrl.IsNullOrWhiteSpace())
        {
            return null;
        }
        var callbackHeaderBuilder =
            new CallbackHeaderBuilder(AliyunObjectStorageOptions.CallbackUrl, AliyunObjectStorageOptions.CallbackBody).Build();
        var metadata = new ObjectMetadata();
        metadata.AddHeader(HttpHeaders.Callback, callbackHeaderBuilder);
        return metadata;
    }

    #endregion

    #region Whether to enable resuming upload（是否启用断点续传）

    /// <summary>
    /// Whether to enable resuming upload
    ///
    /// 是否启用断点续传
    /// </summary>
    /// <param name="streamSize">file stream size（文件流大小）</param>
    /// <returns></returns>
    public bool EnableResumableUpload(long streamSize)
    {
        return AliyunObjectStorageOptions.EnableResumableUpload && AliyunObjectStorageOptions.BigObjectContentLength > streamSize;
    }

    #endregion

}
