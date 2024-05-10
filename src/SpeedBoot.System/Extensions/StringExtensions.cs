// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

public static class StringExtensions
{
#if !(NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1)
    public static bool Contains(this string str, string value, StringComparison stringComparison)
        => str.IndexOf(value, stringComparison) >= 0;
#endif

    public static bool IsNull(
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(false)]
#endif
        this string? value)
        => value == null;

    public static bool IsNullOrWhiteSpace(
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(false)]
#endif
        this string? value)
        => string.IsNullOrWhiteSpace(value);

    public static bool IsNullOrEmpty(
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(false)]
#endif
        this string? value)
        => string.IsNullOrEmpty(value);

    /// <summary>
    /// remove start with <paramref name="trimParameter">trimParameter</paramref> and returns
    /// default: ignore case
    /// </summary>
    /// <param name="value">string to remove</param>
    /// <param name="trimParameter">string ending with what</param>
    /// <returns></returns>
    public static string TrimStart(this string value, string trimParameter)
        => value.TrimStart(trimParameter, StringComparison.CurrentCulture);

    /// <summary>
    /// remove start with <paramref name="trimParameter">trimParameter</paramref> and returns
    /// </summary>
    /// <param name="value">string to remove</param>
    /// <param name="trimParameter">string ending with what</param>
    /// <param name="stringComparison">One of the enumeration values that determines how this string and value are compared.</param>
    /// <returns></returns>
    public static string TrimStart(this string value,
        string trimParameter,
        StringComparison stringComparison)
    {
        return !value.StartsWith(trimParameter, stringComparison) ? value : value.Substring(trimParameter.Length);
    }

    /// <summary>
    /// remove ends with <paramref name="trimParameter">trimParameter</paramref> and returns
    /// default: ignore case
    /// </summary>
    /// <param name="value">string to remove</param>
    /// <param name="trimParameter">string ending with what</param>
    /// <returns></returns>
    public static string TrimEnd(this string value, string trimParameter)
        => value.TrimEnd(trimParameter, StringComparison.CurrentCulture);

    /// <summary>
    /// remove ends with <paramref name="trimParameter">trimParameter</paramref> and returns
    /// </summary>
    /// <param name="value">string to remove</param>
    /// <param name="trimParameter">string ending with what</param>
    /// <param name="stringComparison">One of the enumeration values that determines how this string and value are compared.</param>
    /// <returns></returns>
    public static string TrimEnd(this string value,
        string trimParameter,
        StringComparison stringComparison)
    {
        return !value.EndsWith(trimParameter, stringComparison) ? value : value.Substring(0, value.Length - trimParameter.Length);
    }

    #region get word count（获取字数）

    #region Get the total number of English letters（获取英文字母总数）

    /// <summary>
    /// Get the total number of English letters（ignore case）
    /// 获取英文字母总数（忽略大小写）
    /// </summary>
    /// <param name="str">string to match（待匹配的字符串）</param>
    public static int TotalLetters(this string str) =>
        str.IsNullOrWhiteSpace() ? 0 : str.ToCharArray().Count(char.IsLetter);

    #endregion Get the total number of English letters（获取英文字母总数）

    #region Get the total number of uppercase English letters（获取大写英文字母总数）

    /// <summary>
    /// Get the total number of uppercase English letters
    /// 获取大写英文字母总数
    /// </summary>
    /// <param name="str">string to match（待匹配的字符串）</param>
    public static int TotalUpperLetters(this string str) => str.IsNullOrWhiteSpace()
        ? 0
        : str.ToCharArray().Count(x => char.IsLetter(x) && char.IsUpper(x));

    #endregion Get the total number of English letters（获取英文字母总数）

    #region Get the total number of lowercase English letters（获取小写英文字母总数）

    /// <summary>
    /// Get the total number of lowercase English letters
    /// 获取小写英文字母总数
    /// </summary>
    /// <param name="str">string to match（待匹配的字符串）</param>
    public static int TotalLowerLetters(this string str) => str.IsNullOrWhiteSpace()
        ? 0
        : str.ToCharArray().Count(x => char.IsLetter(x) && char.IsLower(x));

    #endregion Get the total number of lowercase English letters（获取小写英文字母总数）

    #region Get the number of decimal digits（获取十进制数字字数）

    /// <summary>
    /// Gets the number of decimal digits. Example: 0,1,2,3,4,5,6,7,8,9
    /// 获取十进制数字字数。例如：0,1,2,3,4,5,6,7,8,9
    /// </summary>
    /// <param name="str">string to match（待匹配的字符串）</param>
    public static int TotalDigits(this string str) =>
        str.IsNullOrWhiteSpace() ? 0 : str.ToCharArray().Count(char.IsDigit);

    #endregion Get the number of decimal digits（获取十进制数字字数）

    #region Get the number of punctuation characters（获取标点符号字数）

    /// <summary>
    /// Get the number of punctuation characters
    /// 获取标点符号字数
    /// </summary>
    /// <param name="str">string to match（待匹配的字符串）</param>
    /// <returns></returns>
    public static int TotalPunctuation(this string str) =>
        str.IsNullOrWhiteSpace() ? 0 : str.ToCharArray().Count(char.IsPunctuation);

    #endregion Get the number of punctuation characters（获取标点符号字数）

    #region Get the number of delimited characters（获取分隔符号字数）

    /// <summary>
    /// Get the number of delimited characters
    /// 获取分隔符号字数
    /// </summary>
    /// <param name="str">string to match（待匹配的字符串）</param>
    /// <returns></returns>
    public static int TotalSeparator(this string str) =>
        str.IsNullOrWhiteSpace() ? 0 : str.ToCharArray().Count(char.IsSeparator);

    #endregion Get the number of delimited characters（获取分隔符号字数）

    #region Get the word count of symbol characters（获取符号字符字数）

    /// <summary>
    /// Get the word count of symbol characters
    /// 获取符号字符字数
    /// </summary>
    /// <param name="str">string to match（待匹配的字符串）</param>
    /// <returns></returns>
    public static int TotalSymbol(this string str) =>
        str.IsNullOrWhiteSpace() ? 0 : str.ToCharArray().Count(char.IsSymbol);

    #endregion Get the word count of symbol characters（获取符号字符字数）

    #endregion get word count（获取字数）

    #region Get a string of specified length（获取指定长度的字符串）

    /// <summary>
    /// Get a string of specified length（truncated if the length is exceeded, and completed if the length is insufficient）
    ///
    /// 获取指定长度的字符串（超出长度截断，不足补全）
    /// </summary>
    /// <param name="value">string to match（待匹配的字符串）</param>
    /// <param name="length">specified length（指定长度）</param>
    /// <param name="fillPattern">fill Pattern（填充模式）</param>
    /// <param name="fillCharacter">fill character</param>
    /// <param name="errorFunc"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static string GetSpecifiedLengthString(
        this string value,
        int length,
        FillPattern fillPattern = FillPattern.Left,
        char fillCharacter = ' ',
        Func<Exception>? errorFunc = null)
    {
        var keyLength = value.Length;
        if (keyLength == length) return value;

        if (keyLength > length) return value.Substring(0, length);

        switch (fillPattern)
        {
            case FillPattern.Left:
                return value.PadLeft(length, fillCharacter);
            case FillPattern.Right:
                return value.PadRight(length, fillCharacter);
            case FillPattern.NoFill:
            default:
                if (errorFunc == null)
                {
                    throw new NotSupportedException(
                        $"... The length of the string is less than [{length}], the padding mode does not support");
                }
                throw errorFunc();
        }
    }

    #endregion Get a string of specified length（获取指定长度的字符串）

    #region Convert Base64-encoded string to byte array（将 Base64 编码的字符串转换为字节数组）

    /// <summary>
    /// Convert Base64-encoded string to byte array
    ///
    /// 将 Base64 编码的字符串转换为字节数组
    /// </summary>
    /// <param name="value">the string to be converted（待转换的字符串）</param>
    /// <returns></returns>
    public static byte[] FromBase64String(this string value)
        => Convert.FromBase64String(value);

    #endregion

    public static string ToSafeString(this string? value, string defaultValue = "")
        => value.IsNull() ? defaultValue : value!;

    public static string? ToStringOrNull(this string? value, string? defaultValue = null)
        => value.ToStringOrNull(() => defaultValue);

    public static string? ToStringOrNull(this string? value, Func<string?>? defaultFunc)
        => value ?? defaultFunc?.Invoke();

    #region char

    public static bool TryToChar(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out char? result)
    {
        if (value.IsNullOrWhiteSpace() || !char.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static char? ToCharOrNull(this string? value, char? defaultValue = null)
        => value.ToCharOrNull(() => defaultValue);

    public static char? ToCharOrNull(this string? value, Func<char?>? defaultValueFunc)
        => value.TryToChar(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static char ToChar(this string? value, char defaultValue = default)
        => value.ToChar(() => defaultValue);

    public static char ToChar(this string? value, Func<char> defaultValueFunc)
        => value.TryToChar(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? default;

    #endregion

    #region sbyte

    public static bool TryToSByte(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out sbyte? result)
    {
        if (value.IsNullOrWhiteSpace() || !sbyte.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static sbyte? ToSByteOrNull(this string? value, sbyte? defaultValue = null)
        => value.ToSByteOrNull(() => defaultValue);

    public static sbyte? ToSByteOrNull(this string? value, Func<sbyte?>? defaultValueFunc)
        => value.TryToSByte(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static sbyte ToSByte(this string? value, sbyte defaultValue = default)
        => value.ToSByte(() => defaultValue);

    public static sbyte ToSByte(this string? value, Func<sbyte> defaultValueFunc)
        => value.TryToSByte(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region byte

    public static bool TryToByte(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out byte? result)
    {
        if (value.IsNullOrWhiteSpace() || !byte.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static byte? ToByteOrNull(this string? value, byte? defaultValue = null)
        => value.ToByteOrNull(() => defaultValue);

    public static byte? ToByteOrNull(this string? value, Func<byte?>? defaultValueFunc)
        => value.TryToByte(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static byte ToByte(this string? value, byte defaultValue = default)
        => value.ToByte(() => defaultValue);

    public static byte ToByte(this string? value, Func<byte> defaultValueFunc)
        => value.TryToByte(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region ushort

    public static bool TryToUShort(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out ushort? result)
    {
        if (value.IsNullOrWhiteSpace() || !ushort.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static ushort? ToUShortOrNull(this string? value, ushort? defaultValue = null)
        => value.ToUShortOrNull(() => defaultValue);

    public static ushort? ToUShortOrNull(this string? value, Func<ushort?>? defaultValueFunc)
        => value.TryToUShort(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static ushort ToUShort(this string? value, ushort defaultValue = default)
        => value.ToUShort(() => defaultValue);

    public static ushort ToUShort(this string? value, Func<ushort> defaultValueFunc)
        => value.TryToUShort(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region short

    public static bool TryToShort(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out short? result)
    {
        if (value.IsNullOrWhiteSpace() || !short.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static short? ToShortOrNull(this string? value, short? defaultValue = null)
        => value.ToShortOrNull(() => defaultValue);

    public static short? ToShortOrNull(this string? value, Func<short?>? defaultValueFunc)
        => value.TryToShort(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static short ToShort(this string? value, short defaultValue = 0)
        => value.ToShort(() => defaultValue);

    public static short ToShort(this string? value, Func<short> defaultValueFunc)
        => value.TryToShort(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region int

    public static bool TryToInt(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out int? result)
    {
        if (value.IsNullOrWhiteSpace() || !int.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static int? ToIntOrNull(this string? value, int? defaultValue = null)
        => value.ToIntOrNull(() => defaultValue);

    public static int? ToIntOrNull(this string? value, Func<int?>? defaultValueFunc)
        => value.TryToInt(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static int ToInt(this string? value, int defaultValue = 0)
        => value.ToInt(() => defaultValue);

    public static int ToInt(this string? value, Func<int> defaultValueFunc)
        => value.TryToInt(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region long

    public static bool TryToLong(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out long? result)
    {
        if (value.IsNullOrWhiteSpace() || !long.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static long? ToLongOrNull(this string? value, long? defaultValue = null)
        => value.ToLongOrNull(() => defaultValue);

    public static long? ToLongOrNull(this string? value, Func<long?>? defaultValueFunc)
        => value.TryToLong(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static long ToLong(this string? value, long defaultValue = 0)
        => value.ToLong(() => defaultValue);

    public static long ToLong(this string? value, Func<long> defaultValueFunc)
        => value.TryToLong(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region decimal

    public static bool TryToDecimal(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out decimal? result)
    {
        if (value.IsNullOrWhiteSpace() || !decimal.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static decimal? ToDecimalOrNull(this string? value, decimal? defaultValue = null)
        => value.ToDecimalOrNull(() => defaultValue);

    public static decimal? ToDecimalOrNull(this string? value, Func<decimal?>? defaultValueFunc)
        => value.TryToDecimal(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static decimal ToDecimal(this string? value, decimal defaultValue = 0)
        => value.ToDecimal(() => defaultValue);

    public static decimal ToDecimal(this string? value, Func<decimal> defaultValueFunc)
        => value.TryToDecimal(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region float

    public static bool TryToFloat(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out float? result)
    {
        if (value.IsNullOrWhiteSpace() || !float.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static float? ToFloatOrNull(this string? value, float? defaultValue = null)
        => value.ToFloatOrNull(() => defaultValue);

    public static float? ToFloatOrNull(this string? value, Func<float?>? defaultValueFunc)
        => value.TryToFloat(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static float ToFloat(this string? value, float defaultValue = 0)
        => value.ToFloat(() => defaultValue);

    public static float ToFloat(this string? value, Func<float> defaultValueFunc)
        => value.TryToFloat(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region double

    public static bool TryToDouble(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out double? result)
    {
        if (value.IsNullOrWhiteSpace() || !double.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static double? ToDoubleOrNull(this string? value, double? defaultValue = null)
        => value.ToDoubleOrNull(() => defaultValue);

    public static double? ToDoubleOrNull(this string? value, Func<double?>? defaultValueFunc)
        => value.TryToDouble(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static double ToDouble(this string? value, double defaultValue)
        => value.ToDouble(() => defaultValue);

    public static double ToDouble(this string? value, Func<double> defaultValueFunc)
        => value.TryToDouble(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? 0;

    #endregion

    #region bytes

    public static bool TryToBytes(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out byte[]? result)
        => value.TryToBytes(Encoding.UTF8, out result);

    public static bool TryToBytes(this string? value,
        Encoding encoding,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out byte[]? result)
    {
        if (value.IsNullOrWhiteSpace())
        {
            result = default;
            return false;
        }

        result = encoding.GetBytes(value!);
        return true;
    }

    public static byte[]? ToBytesOrNull(this string? value, byte[]? defaultValue)
        => value.ToBytesOrNull(defaultValue, Encoding.UTF8);

    public static byte[]? ToBytesOrNull(this string? value, byte[]? defaultValue, Encoding encoding)
        => value.ToBytesOrNull(() => defaultValue, encoding);

    public static byte[]? ToBytesOrNull(this string? value, Func<byte[]?>? defaultValueFunc, Encoding encoding)
        => value.TryToBytes(encoding, out var result) ? result : defaultValueFunc?.Invoke();

    public static byte[] ToBytes(this string? value)
        => value.ToBytes(Encoding.UTF8);

    public static byte[] ToBytes(this string? value, Encoding encoding)
        => value.ToBytes(() => Array.Empty<byte>(), encoding);

    public static byte[] ToBytes(this string? value, byte[] defaultValue, Encoding encoding)
        => value.ToBytes(() => defaultValue, encoding);

    public static byte[] ToBytes(this string? value, Func<byte[]> defaultValueFunc, Encoding encoding)
        => value.TryToBytes(encoding, out var result) ? result! : defaultValueFunc?.Invoke() ?? Array.Empty<byte>();

    #endregion

    #region datetime

    public static bool TryToDateTime(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out DateTime? result)
    {
        if (value.IsNullOrWhiteSpace() || !DateTime.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static DateTime? ToDateTimeOrNull(this string? value, DateTime? defaultValue = null)
        => value.ToDateTimeOrNull(() => defaultValue);

    public static DateTime? ToDateTimeOrNull(this string? value, Func<DateTime?>? defaultValueFunc)
        => value.TryToDateTime(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static DateTime ToDateTime(this string? value)
        => value.ToDateTime(() => new DateTime());

    public static DateTime ToDateTime(this string? value, DateTime defaultValue)
        => value.ToDateTime(() => defaultValue);

    public static DateTime ToDateTime(this string? value, Func<DateTime> defaultValueFunc)
        => value.TryToDateTime(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? new DateTime();

    #endregion

    #region bool

    private static readonly Dictionary<string, bool> BoolMap = new(StringComparer.OrdinalIgnoreCase)
    {
        { "0", false },
        { "false", false },
        { "不", false },
        { "否", false },
        { "失败", false },
        { "no", false },
        { "fail", false },
        { "lose", false },
        { "true", true },
        { "1", true },
        { "是", true },
        { "ok", true },
        { "yes", true },
        { "success", true },
        { "成功", true }
    };

    public static bool TryToBool(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out bool? result)
    {
        if (!value.IsNullOrWhiteSpace() &&
            (BoolMap.TryGetValue(value, out var valResult) || bool.TryParse(value, out valResult)))
        {
            result = valResult;
            return true;
        }
        result = default;
        return false;
    }

    public static bool? ToBoolOrNull(this string? value, bool? defaultValue = null)
        => value.ToBoolOrNull(() => defaultValue);

    public static bool? ToBoolOrNull(this string? value, Func<bool?>? defaultValueFunc)
        => value.TryToBool(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static bool ToBool(this string? value, bool defaultValue = false)
        => value.ToBool(() => defaultValue);

    public static bool ToBool(this string? value, Func<bool> defaultValueFunc)
        => value.TryToBool(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? false;

    #endregion

    #region Guid

    public static bool TryToGuid(this string? value,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out Guid? result)
    {
        if (value.IsNullOrWhiteSpace() || !Guid.TryParse(value, out var valResult))
        {
            result = default;
            return false;
        }
        result = valResult;
        return true;
    }

    public static Guid? ToGuidOrNull(this string? value, Guid? defaultValue = null)
        => value.ToGuidOrNull(() => defaultValue);

    public static Guid? ToGuidOrNull(this string? value, Func<Guid?>? defaultValueFunc)
        => value.TryToGuid(out var result) ? result!.Value : defaultValueFunc?.Invoke();

    public static Guid ToGuid(this string? value)
        => value.ToGuid(() => Guid.Empty);

    public static Guid ToGuid(this string? value, Guid defaultValue)
        => value.ToGuid(() => defaultValue);

    public static Guid ToGuid(this string? value, Func<Guid> defaultValueFunc)
        => value.TryToGuid(out var result) ? result!.Value : defaultValueFunc?.Invoke() ?? Guid.Empty;

    #endregion

    #region Stream

    public static Stream ToStream(this string value)
        => value.ToStreamOrNull() ?? Stream.Null;

    public static Stream? ToStreamOrNull(this string? value)
    {
        if (value.IsNullOrWhiteSpace())
            return default;

        var bytes = value.ToBytes();
        return new MemoryStream(bytes);
    }

    #endregion

    static readonly Dictionary<Type, Func<string, (bool State, object? Value)>> _converterData = new()
    {
        { typeof(string), str => (true, str) },
        { typeof(short), str => (short.TryParse(str, out var val), val) },
        { typeof(short?), str => short.TryParse(str, out var val) ? (true, val) : (false, default(short?)) },
        { typeof(int), str => (int.TryParse(str, out var val), val) },
        { typeof(int?), str => int.TryParse(str, out var val) ? (true, val) : (false, default(int?)) },
        { typeof(long), str => (long.TryParse(str, out var val), val) },
        { typeof(long?), str => long.TryParse(str, out var val) ? (true, val) : (false, default(long?)) },
        { typeof(float), str => (long.TryParse(str, out var val), val) },
        { typeof(float?), str => long.TryParse(str, out var val) ? (true, val) : (false, default(float?)) },
        { typeof(decimal), str => (decimal.TryParse(str, out var val), val) },
        { typeof(decimal?), str => decimal.TryParse(str, out var val) ? (true, val) : (false, default(decimal?)) },
        { typeof(double), str => (double.TryParse(str, out var val), val) },
        { typeof(double?), str => double.TryParse(str, out var val) ? (true, val) : (false, default(double?)) },
        { typeof(bool), str => (bool.TryParse(str, out var val), val) },
        { typeof(bool?), str => bool.TryParse(str, out var val) ? (true, val) : (false, default(bool?)) },
        { typeof(char), str => (char.TryParse(str, out var val), val) },
        { typeof(char?), str => char.TryParse(str, out var val) ? (true, val) : (false, default(char?)) },
        { typeof(byte), str => (byte.TryParse(str, out var val), val) },
        { typeof(byte?), str => byte.TryParse(str, out var val) ? (true, val) : (false, default(byte?)) },
        { typeof(sbyte), str => (sbyte.TryParse(str, out var val), val) },
        { typeof(sbyte?), str => sbyte.TryParse(str, out var val) ? (true, val) : (false, default(sbyte?)) },
        { typeof(ushort), str => (ushort.TryParse(str, out var val), val) },
        { typeof(ushort?), str => ushort.TryParse(str, out var val) ? (true, val) : (false, default(ushort?)) },
        { typeof(uint), str => (uint.TryParse(str, out var val), val) },
        { typeof(uint?), str => uint.TryParse(str, out var val) ? (true, val) : (false, default(uint?)) },
        { typeof(ulong), str => (ulong.TryParse(str, out var val), val) },
        { typeof(ulong?), str => ulong.TryParse(str, out var val) ? (true, val) : (false, default(ulong?)) },
        { typeof(Guid), str => (Guid.TryParse(str, out var val), val) },
        { typeof(Guid?), str => Guid.TryParse(str, out var val) ? (true, val) : (false, default(Guid?)) },
        { typeof(DateTime), str => (DateTime.TryParse(str, out var val), val) },
        { typeof(DateTime?), str => DateTime.TryParse(str, out var val) ? (true, val) : (false, default(DateTime?)) },
#if NET6_0_OR_GREATER
        { typeof(TimeOnly), str => (TimeOnly.TryParse(str, out var val), val) },
        { typeof(TimeOnly?), str => TimeOnly.TryParse(str, out var val) ? (true, val) : (false, default(TimeOnly?)) },
        { typeof(DateOnly), str => (DateOnly.TryParse(str, out var val), val) },
        { typeof(DateOnly?), str => DateOnly.TryParse(str, out var val) ? (true, val) : (false, default(DateOnly?)) },
#endif
    };

    /// <summary>
    /// Complex types are not supported
    /// support types: string, short, int, long, float, decimal, double, bool, char, byte, sbyte, ushort, uint, ulong, Guid, DateTime,DateOnly,TimeOnly
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryConvertTo(
        this string? value,
        Type type,
#if NETCOREAPP3_0_OR_GREATER
        [NotNullWhen(true)]
#endif
        out object? result)
    {
        if (value == null)
        {
            result = null;
            return false;
        }
        if(!_converterData.TryGetValue(type, out var func))
            throw new NotSupportedException($"not supported type: {type}");

        var res = func.Invoke(value);
        result = res.Value;
        return res.State;
    }

    public static object ConvertTo(
        this string value,
        Type type)
    {
        if(!_converterData.TryGetValue(type, out var func))
            throw new NotSupportedException($"not supported type: {type}");

        return func.Invoke(value).Value;
    }
}
