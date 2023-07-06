// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.ObjectStorage.Aliyun;

/// <summary>
/// aliyun Sts Options
///
/// 阿里云 Sts 配置
/// </summary>
public class AliyunStsOptions
{
    /// <summary>
    /// sts region id
    /// If the RegionId is missing, the temporary Sts credential cannot be obtained.
    /// https://www.alibabacloud.com/help/en/resource-access-management/latest/endpoints#reference-sdg-3pv-xdb
    /// example: cn-hongkong
    ///
    /// sts区域id
    /// 如果RegionId缺失，则无法获取临时Sts凭证。
    /// https://help.aliyun.com/document_detail/371859.html
    /// 例如：cn-hongkong
    /// </summary>
    public string? RegionId { get; set; }

    private long _durationSeconds = 3600;

    /// <summary>
    /// Set the validity period of the temporary access credential, the minimum is 900, and the maximum is 43200.
    /// default: 3600
    /// unit: second
    ///
    /// 设置临时访问凭证的有效期，最小为900，最大为43200。
    /// 默认：3600
    /// 单位：秒
    /// </summary>
    public long DurationSeconds
    {
        get => _durationSeconds;
        set
        {
            if (value is < 900 or > 43200)
                throw new ArgumentOutOfRangeException(nameof(DurationSeconds), $"{nameof(DurationSeconds)} must be in range of 900-43200");

            _durationSeconds = value;
        }
    }

    /// <summary>
    /// If policy is empty, the user will get all permissions under this role
    ///
    /// 如果policy为空，则用户将获得该角色下的所有权限
    /// </summary>
    public string Policy { get; set; }

    public string RoleArn { get; set; }

    public string RoleSessionName { get; set; }

    private long _earlyExpires = 10;

    /// <summary>
    /// Voucher expires early
    /// default: 10
    /// unit: second
    ///
    /// 凭证提前过期时间
    /// 默认：10
    /// 单位：秒
    /// </summary>
    public long EarlyExpires
    {
        get => _earlyExpires;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(EarlyExpires), $"{nameof(EarlyExpires)} must be Greater than 0");

            _earlyExpires = value;
        }
    }
}
