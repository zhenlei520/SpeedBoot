// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography;

/// <summary>
/// Unicode Encode and Decode
/// </summary>
public class UnicodeUtils
{
    /// <summary>
    /// Unicode Encode
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <returns>encrypted data</returns>
    public static string Encode(string content)
    {
        var stringBuilder = new StringBuilder();
        foreach (var c in content)
            stringBuilder.Append("\\u" + ((int)c).ToString("x4"));

        return stringBuilder.ToString();
    }

    /// <summary>
    /// Unicode Decode
    /// </summary>
    /// <param name="content">String to decrypt</param>
    /// <returns>decrypted data</returns>
    public static string Decode(string content)
        => Regex.Unescape(content);
}
