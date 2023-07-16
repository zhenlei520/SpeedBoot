// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography;

/// <summary>
/// 全局加密配置
/// </summary>
public static class GlobalCryptographyConfig
{
    /// <summary>
    /// 默认编码方式
    /// </summary>
    public static Encoding DefaultEncoding = Encoding.UTF8;

    #region AES

    #region 密钥

    private static string InitDefaultAesKey()
    {
        var aesKey = Environment.GetEnvironmentVariable("SpeedBoot:AesKey");
        if (aesKey.IsNullOrWhiteSpace())
        {
            aesKey = "speedboot";
        }
        return aesKey.GetSpecifiedLengthString(DefaultAesKeyLength, FillPattern.Right);
    }

    private static string _defaultAesKey = InitDefaultAesKey();

    /// <summary>
    /// 默认AES 秘钥
    /// </summary>
    public static string DefaultAesKey
    {
        get => _defaultAesKey;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameof(DefaultAesKey)} cannot be empty", nameof(DefaultAesKey));

            _defaultAesKey = value;
        }
    }

    #endregion

    #region IV

    private static string InitDefaultAesIV()
    {
        var aesIv = Environment.GetEnvironmentVariable("SpeedBoot:AesIV");
        if (aesIv.IsNullOrWhiteSpace())
        {
            aesIv = "speedboot";
        }
        return aesIv.GetSpecifiedLengthString(16, FillPattern.Right);
    }

    private static string _defaultAesIv = InitDefaultAesIV();

    /// <summary>
    /// 默认AES 秘钥
    /// </summary>
    public static string DefaultAesIv
    {
        get => _defaultAesIv;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameof(DefaultAesIv)} cannot be empty", nameof(DefaultAesIv));

            _defaultAesIv = value;
        }
    }

    #endregion

    #region 密钥长度

    private static int InitDefaultAesKeyLength()
    {
        var aesLength = Environment.GetEnvironmentVariable("SpeedBoot:AesLength");
        if (aesLength.IsNullOrWhiteSpace())
        {
            return 32;
        }
        return DefaultAesKeyLength = int.Parse(aesLength);
    }

    private static int _defaultAesKeyLength = InitDefaultAesKeyLength();

    /// <summary>
    /// 默认AES 秘钥长度
    /// 仅支持16、24、32
    /// </summary>
    public static int DefaultAesKeyLength
    {
        get => _defaultAesKeyLength;
        set
        {
            if (value != 16 && value != 24 && value != 32)
                throw new ArgumentException("Aes key length can only be 16, 24 or 32 bits", nameof(DefaultAesKeyLength));

            _defaultAesKeyLength = value;
        }
    }

    #endregion

    #endregion

    #region DES

    #region 密钥

    private static string InitDefaultDesKey()
    {
        var aesKey = Environment.GetEnvironmentVariable("SpeedBoot:DesKey");
        if (aesKey.IsNullOrWhiteSpace())
        {
            aesKey = "speedboo";
        }
        return aesKey.GetSpecifiedLengthString(8, FillPattern.Right);
    }

    private static string _defaultDesKey = InitDefaultDesKey();

    /// <summary>
    /// 默认AES 秘钥
    /// </summary>
    public static string DefaultDesKey
    {
        get => _defaultDesKey;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameof(DefaultDesKey)} cannot be empty", nameof(DefaultDesKey));

            _defaultDesKey = value;
        }
    }

    #endregion

    #region IV

    private static string InitDefaultDesIV()
    {
        var aesIv = Environment.GetEnvironmentVariable("SpeedBoot:DesIV");
        if (aesIv.IsNullOrWhiteSpace())
        {
            aesIv = "speedboot";
        }
        return aesIv.GetSpecifiedLengthString(8, FillPattern.Right);
    }

    private static string _defaultDesIv = InitDefaultDesIV();

    /// <summary>
    /// 默认AES 秘钥
    /// </summary>
    public static string DefaultDesIv
    {
        get => _defaultDesIv;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameof(DefaultDesIv)} cannot be empty", nameof(DefaultDesIv));

            _defaultDesIv = value;
        }
    }

    #endregion

    #endregion
}
