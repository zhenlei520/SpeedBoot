// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

public class SpeedOptions
{
    public bool EnabledServiceComponent { get; set; } = true;

    /// <summary>
    /// Assembly name prefix
    /// default: *
    /// Support for regular expressions
    /// </summary>
    public string AssemblyName { get; set; } = "*";
}
