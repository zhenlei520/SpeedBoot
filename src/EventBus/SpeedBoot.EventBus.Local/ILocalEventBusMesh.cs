﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public interface ILocalEventBusMesh
{
    public Dictionary<Type, DispatchRecord> MeshData { get; set; }
}
