// Copyright (c) MASA Stack All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Security.Cryptography;

// ReSharper disable once InconsistentNaming
public enum DESEncryptType
{
    /// <summary>
    /// original DES encryption
    /// </summary>
    Normal,

    /// <summary>
    /// Easy to transfer in browser
    /// </summary>
    Improved
}
