// Copyright (c) MASA Stack All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Security.Cryptography;

/// <summary>
/// AES encryption and decryption
/// </summary>
public class AesUtils : EncryptBase
{
    private static readonly string DefaultEncryptKey = InitDefaultEncryptKey();

    private static readonly string DefaultEncryptIv = GlobalConfigurationUtils.DefaultAesEncryptIv;

    private static int DefaultEncryptKeyLength => GlobalConfigurationUtils.DefaultAesEncryptKeyLength;

    static string InitDefaultEncryptKey()
    {
        return GlobalConfigurationUtils.DefaultAesEncryptKey.GetSpecifiedLengthString(
            DefaultEncryptKeyLength, FillPattern.Right, ' ');
    }

    #region Encrypt

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged encryption (RijndaelManaged (AES) algorithm is a block encryption algorithm)
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>encrypted result</returns>
    public static string Encrypt(
        string content,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => Encrypt(content, DefaultEncryptKey, FillPattern.Right, fillCharacter, encoding);

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged encryption (RijndaelManaged (AES) algorithm is a block encryption algorithm)
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="key">Encryption key, must have half-width characters. 16-bit or 24-bit or 32-bit length key or complement by fillPattern to calculate an 16-bit or 24-bit or 32-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>encrypted result</returns>
    public static string Encrypt(
        string content,
        string key,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
        => Encrypt(content, key, DefaultEncryptIv, fillPattern, fillCharacter, encoding, aesLength);

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged encryption (RijndaelManaged (AES) algorithm is a block encryption algorithm)
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="key">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="iv">16-bit length key or complement by fillPattern to calculate an 16-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys or 16-bit iv)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>encrypted result</returns>
    public static string Encrypt(
        string content,
        string key,
        string iv,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        var byteBuffer = EncryptToBytes(
            currentEncoding.GetBytes(content),
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter)
        );
        return byteBuffer.ToBase64String();
    }

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged encryption (RijndaelManaged (AES) algorithm is a block encryption algorithm)
    /// </summary>
    /// <param name="content">String to be encrypted</param>
    /// <param name="key">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="ivBuffer">16-bit length or complement by fillPattern to calculate an 16-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>encrypted result</returns>
    public static string Encrypt(
        string content,
        string key,
        byte[] ivBuffer,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        var byteBuffer = EncryptToBytes(
            currentEncoding.GetBytes(content),
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            ivBuffer);
        return byteBuffer.ToBase64String();
    }

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged encryption (RijndaelManaged (AES) algorithm is a block encryption algorithm)
    /// </summary>
    /// <param name="dataBuffers">data array to be encrypted</param>
    /// <param name="keyBuffer">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="ivBuffer">16-bit length or complement by fillPattern to calculate an 16-bit string</param>
    /// <returns>encrypted result</returns>
    public static byte[] EncryptToBytes(
        byte[] dataBuffers,
        byte[] keyBuffer,
        byte[] ivBuffer)
        => EncryptOrDecryptToBytes(dataBuffers, keyBuffer, ivBuffer, true);

    /// <summary>
    /// encrypted stream
    /// </summary>
    /// <param name="stream">streams that require encryption</param>
    /// <param name="key">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>Returns the encrypted byte array</returns>
    public static byte[] EncryptToBytes(
        Stream stream,
        string key,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
        => EncryptToBytes(stream, key, DefaultEncryptIv, fillPattern, fillCharacter, encoding, aesLength);

    /// <summary>
    /// encrypted stream
    /// </summary>
    /// <param name="stream">streams that require encryption</param>
    /// <param name="key">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="iv">16-bit length. 16-bit length key or complement by fillPattern to calculate an 16-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>Returns the encrypted byte array</returns>
    public static byte[] EncryptToBytes(
        Stream stream,
        string key,
        string iv,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);

        return EncryptToBytes(stream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter));
    }

    /// <summary>
    /// encrypted stream
    /// </summary>
    /// <param name="stream">streams that require encryption</param>
    /// <param name="key">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="ivBuffer">16-bit length</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>Returns the encrypted byte array</returns>
    public static byte[] EncryptToBytes(
        Stream stream,
        string key,
        byte[] ivBuffer,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        return EncryptToBytes(
            stream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            ivBuffer);
    }

    /// <summary>
    /// encrypted stream
    /// </summary>
    /// <param name="stream">streams that require encryption</param>
    /// <param name="keyBuffer">Encryption key, 32-bit length</param>
    /// <param name="ivBuffer">16-bit length</param>
    /// <returns>Returns the encrypted byte array</returns>
    public static byte[] EncryptToBytes(
        Stream stream,
        byte[] keyBuffer,
        byte[] ivBuffer)
        => EncryptOrDecryptToBytes(stream, keyBuffer, ivBuffer, true);

    /// <summary>
    /// Encrypt the specified stream with AES and output a file
    /// </summary>
    /// <param name="stream">stream to be encrypted</param>
    /// <param name="outputPath">output file path</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    public static void EncryptFile(
        Stream stream,
        string outputPath,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => EncryptFile(stream,
            DefaultEncryptKey,
            outputPath,
            FillPattern.Right,
            fillCharacter,
            encoding);

    /// <summary>
    /// Encrypt the specified stream with AES and output a file
    /// </summary>
    /// <param name="stream">stream to be encrypted</param>
    /// <param name="key">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="outputPath">output file path</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys or 16-bit iv)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    public static void EncryptFile(
        Stream stream,
        string key,
        string outputPath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
        => EncryptFile(stream, key, DefaultEncryptIv, outputPath, fillPattern, fillCharacter, encoding, aesLength);

    /// <summary>
    /// Encrypt the specified stream with AES and output a file
    /// </summary>
    /// <param name="stream">stream to be encrypted</param>
    /// <param name="key">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="ivBuffer">16-bit length</param>
    /// <param name="outputPath">output file path</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys or 16-bit iv)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    public static void EncryptFile(
        Stream stream,
        string key,
        byte[] ivBuffer,
        string outputPath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        EncryptOrDecryptFile(
            stream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            ivBuffer,
            true,
            outputPath);
    }

    /// <summary>
    /// Encrypt the specified stream with AES and output a file
    /// </summary>
    /// <param name="stream">stream to be encrypted</param>
    /// <param name="key">Encryption key, must have half-width characters. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="iv">16-bit length or complement by fillPattern to calculate an 16-bit string</param>
    /// <param name="outputPath">output file path</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys or 16-bit iv)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    public static void EncryptFile(
        Stream stream,
        string key,
        string iv,
        string outputPath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        EncryptOrDecryptFile(
            stream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter),
            true,
            outputPath);
    }

    #endregion

    #region Decrypt

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged decrypts the string
    /// </summary>
    /// <param name="content">String to be decrypted</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <returns>If the decryption succeeds, the decrypted string will be returned, and if it fails, the source string will be returned.</returns>
    public static string Decrypt(
        string content,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => Decrypt(content, DefaultEncryptKey, FillPattern.Right, fillCharacter, encoding);

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged decrypts the string
    /// </summary>
    /// <param name="content">String to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>Decryption success returns the decrypted string, failure returns empty</returns>
    public static string Decrypt(
        string content,
        string key,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
        => Decrypt(content, key, DefaultEncryptIv, fillPattern, fillCharacter, encoding, aesLength);

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged decrypts the string
    /// </summary>
    /// <param name="content">String to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="iv">16-bit length or complement by fillPattern to calculate an 16-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>Decryption success returns the decrypted string, failure returns empty</returns>
    public static string Decrypt(
        string content,
        string key,
        string iv,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        var decryptBuffer = DecryptToBytes(
            content.FromBase64String(),
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter));
        return decryptBuffer.ToString(currentEncoding);
    }

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged decrypts the string
    /// </summary>
    /// <param name="content">String to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="ivBuffer">16-bit length. 16-bit length key or complement by fillPattern to calculate an 16-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>Decryption success returns the decrypted string, failure returns empty</returns>
    public static string Decrypt(
        string content,
        string key,
        byte[] ivBuffer,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        var decryptBuffer = DecryptToBytes(
            content.FromBase64String(),
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            ivBuffer
        );
        return decryptBuffer.ToString(currentEncoding);
    }

    /// <summary>
    /// Symmetric encryption algorithm AES RijndaelManaged decrypts
    /// </summary>
    /// <param name="dataBuffers">data array to be decrypted</param>
    /// <param name="keyBuffer">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="ivBuffer">16-bit length. 16-bit length key or complement by fillPattern to calculate an 16-bit string</param>
    /// <returns></returns>
    public static byte[] DecryptToBytes(
        byte[] dataBuffers,
        byte[] keyBuffer,
        byte[] ivBuffer)
        => EncryptOrDecryptToBytes(
            dataBuffers,
            keyBuffer,
            ivBuffer,
            false);

    /// <summary>
    /// Decrypt the stream
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>returns the decrypted byte array</returns>
    public static byte[] DecryptToBytes(
        Stream stream,
        string key,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
        => DecryptToBytes(stream, key, DefaultEncryptIv, fillPattern, fillCharacter, encoding, aesLength);

    /// <summary>
    /// Decrypt the stream
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="iv">16-bit length</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>returns the decrypted byte array</returns>
    public static byte[] DecryptToBytes(
        Stream stream,
        string key,
        string iv,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        return DecryptToBytes(
            stream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter));
    }

    /// <summary>
    /// Decrypt the stream
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="ivBuffer">16-bit length</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    /// <returns>returns the decrypted byte array</returns>
    public static byte[] DecryptToBytes(
        Stream stream,
        string key,
        byte[] ivBuffer,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        return DecryptToBytes(stream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            ivBuffer);
    }

    /// <summary>
    /// Decrypt the stream
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="keyBuffer">Decryption key, 32-bit length</param>
    /// <param name="ivBuffer">16-bit length</param>
    /// <returns>returns the decrypted byte array</returns>
    public static byte[] DecryptToBytes(
        Stream stream,
        byte[] keyBuffer,
        byte[] ivBuffer)
        => EncryptOrDecryptToBytes(stream, keyBuffer, ivBuffer, false);

    /// <summary>
    /// AES decrypt the specified stream and output the file
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="outputPath">output file path</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys or 16-bit iv)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    public static void DecryptFile(
        Stream stream,
        string outputPath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null)
        => DecryptFile(stream, DefaultEncryptKey, outputPath, fillPattern, fillCharacter, encoding);

    /// <summary>
    /// AES decrypt the specified stream and output the file
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="outputPath">output file path</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys or 16-bit iv)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    public static void DecryptFile(
        Stream stream,
        string key,
        string outputPath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
        => DecryptFile(stream, key, DefaultEncryptIv, outputPath, fillPattern, fillCharacter, encoding, aesLength);

    /// <summary>
    /// AES decrypt the specified stream and output the file
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="ivBuffer">16-bit length</param>
    /// <param name="outputPath">output file path</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys or 16-bit iv)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    public static void DecryptFile(
        Stream stream,
        string key,
        byte[] ivBuffer,
        string outputPath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        EncryptOrDecryptFile(stream, GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength), ivBuffer, false,
            outputPath);
    }

    /// <summary>
    /// AES decrypt the specified stream and output the file
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="key">Decryption key, same as encryption key. 32-bit length key or complement by fillPattern to calculate an 32-bit string</param>
    /// <param name="iv">16-bit length or complement by fillPattern to calculate an 16-bit string</param>
    /// <param name="outputPath">output file path</param>
    /// <param name="fillPattern">Whether to complement the key? default: no fill(Only supports 32-bit keys or 16-bit iv)</param>
    /// <param name="fillCharacter">character for complement</param>
    /// <param name="encoding">Encoding format, default UTF-8</param>
    /// <param name="aesLength">Aes key length</param>
    public static void DecryptFile(
        Stream stream,
        string key,
        string iv,
        string outputPath,
        FillPattern fillPattern = FillPattern.NoFill,
        char fillCharacter = ' ',
        Encoding? encoding = null,
        int? aesLength = null)
    {
        var currentEncoding = GetSafeEncoding(encoding);
        EncryptOrDecryptFile(
            stream,
            GetKeyBuffer(key, currentEncoding, fillPattern, fillCharacter, aesLength),
            GetIvBuffer(iv, currentEncoding, fillPattern, fillCharacter),
            false,
            outputPath);
    }

    #endregion

    #region private methods

    private static byte[] GetKeyBuffer(string key,
        Encoding encoding,
        FillPattern fillPattern,
        char fillCharacter,
        int? aesLength = null)
    {
        var paramName = nameof(key);
        if (aesLength != null && aesLength != 16 && aesLength != 24 && aesLength != 32)
            throw new ArgumentException("Aes key length can only be 16, 24 or 32 bits", nameof(aesLength));

        var length = aesLength ?? DefaultEncryptKeyLength;
        return GetBytes(
            key,
            encoding,
            fillPattern,
            fillCharacter,
            length,
            () => new ArgumentException($"Please enter a {length}-bit AES key or allow {nameof(fillPattern)} to Left or Right",
                paramName));
    }

    private static byte[] GetIvBuffer(string iv,
        Encoding encoding,
        FillPattern fillPattern,
        char fillCharacter)
    {
        var paramName = nameof(iv);
        return GetBytes(
            iv,
            encoding,
            fillPattern,
            fillCharacter,
            16,
            () => new ArgumentException($"Please enter a 16-bit iv or allow {nameof(fillPattern)} to Left or Right", paramName));
    }

    public static byte[] EncryptOrDecryptToBytes(
        byte[] dataBuffers,
        byte[] keyBuffer,
        byte[] ivBuffer,
        bool isEncrypt)
    {
        using var aes = Aes.Create();
        var transform = GetTransform(aes, keyBuffer, ivBuffer, isEncrypt);
        return transform.TransformFinalBlock(dataBuffers, 0, dataBuffers.Length);
    }

    /// <summary>
    /// Decrypt the stream
    /// </summary>
    /// <param name="stream">stream to be decrypted</param>
    /// <param name="keyBuffer">Decryption key, 32-bit length</param>
    /// <param name="ivBuffer">16-bit length</param>
    /// <param name="isEncrypt">encrypt (true) or decrypt (false)</param>
    /// <returns>returns the decrypted byte array</returns>
    private static byte[] EncryptOrDecryptToBytes(
        Stream stream,
        byte[] keyBuffer,
        byte[] ivBuffer,
        bool isEncrypt)
    {
        using var aes = Aes.Create();
        var transform = GetTransform(aes, keyBuffer, ivBuffer, isEncrypt);
        using var ms = new MemoryStream();
        var cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);
        var cipherBytes = stream.ToBytes();
        cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
        cryptoStream.FlushFinalBlock();
        cryptoStream.Close();
        return ms.ToArray();
    }

    private static void EncryptOrDecryptFile(
        Stream stream,
        byte[] keyBuffer,
        byte[] ivBuffer,
        bool isEncrypt,
        string outputPath)
    {
        using var fileStreamOut = new FileStream(outputPath, FileMode.Create);
        using var aes = Aes.Create();
        var transform = GetTransform(aes, keyBuffer, ivBuffer, isEncrypt);
        EncryptOrDecryptFile(stream, fileStreamOut, transform);
    }

    #endregion

}
