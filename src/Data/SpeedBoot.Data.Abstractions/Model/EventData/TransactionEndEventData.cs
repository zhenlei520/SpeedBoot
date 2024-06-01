// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.Abstractions;

public class TransactionEndEventData : TransactionEventDataBase
{
    public TimeSpan Duration { get; set; }
}
