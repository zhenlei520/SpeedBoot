// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

public static class StringExtension
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

    #region String to byte array（字符串转字节数组）

    /// <summary>
    /// String to byte array（default encoding format UTF-8）
    /// 字符串转字节数组（默认编码格式 UTF-8）
    /// </summary>
    /// <param name="value">the string to be converted（待转换的字符串）</param>
    /// <returns></returns>
    public static byte[] ToGetBytes(this string value)
        => Encoding.UTF8.GetBytes(value);

    /// <summary>
    /// String to byte array
    /// 字符串转字节数组
    /// </summary>
    /// <param name="value">the string to be converted（待转换的字符串）</param>
    /// <param name="encoding">Encoding format</param>
    /// <returns></returns>
    public static byte[] ToGetBytes(this string value, Encoding encoding)
        => encoding.GetBytes(value);

    #endregion

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

    public static string SafeString(this string? value)
        => value.IsNull() ? string.Empty : value;

    public static string? WhereIfNull(this string? value, Func<string?> defaultFunc)
        => value ?? defaultFunc.Invoke();
}
