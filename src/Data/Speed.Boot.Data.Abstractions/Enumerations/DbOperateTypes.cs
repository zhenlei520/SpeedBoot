// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed.Boot.Data.Abstractions;

/// <summary>
/// 操作类型
/// </summary>
[Flags]
public enum DbOperateTypes
{
    Read = 1,
    Write = 2
}
