// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore.DataFiltering.Internal;

internal class DataFilterState
{
    public bool Enabled { get; set; }

    public DataFilterState(bool enabled)
    {
        Enabled = enabled;
    }
}
