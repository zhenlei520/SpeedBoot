// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography;

/// <summary>
/// Ascii Encode and Decode
/// </summary>
public class AsciiUtils
{
    /// <summary>
    /// Ascii Encode
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>encrypted data</returns>
    public static string Encode(string content, Encoding? encoding = null)
    {
        var stringBuilder = new StringBuilder();
        foreach (var c in content)
        {
            stringBuilder.Append((int)c);
            stringBuilder.Append(' ');
        }
        return stringBuilder.ToString().Trim();
    }

    /// <summary>
    /// Ascii Decode
    /// </summary>
    /// <param name="content">String to decrypt</param>
    /// <returns>decrypted data</returns>
    public static string Decode(string content)
    {
        var asciiValues = content.Split(' ');
        var stringBuilder = new StringBuilder();
        foreach (var ascii in asciiValues)
            stringBuilder.Append((char)int.Parse(ascii));

        return stringBuilder.ToString();
    }
}
