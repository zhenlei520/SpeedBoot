// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.ObjectStorage.Aliyun.Internal.Options;

/// <summary>
/// aliyun ObjectStorage Options
/// 对象存储配置
/// </summary>
public class AliyunOptions : Aliyun.AliyunOptions
{
    /// <summary>
    /// Aliyun Temporary Access Credentials
    /// 阿里云临时访问凭证
    /// </summary>
    public AliyunStsOptions? Sts { get; set; }
}
