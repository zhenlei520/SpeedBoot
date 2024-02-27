// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public static class GlobalConfig
{
    /// <summary>
    /// Default Assemblies Collection
    /// </summary>
    public static Assembly[] DefaultAssemblies = AppDomain.CurrentDomain.GetAssemblies();
}
