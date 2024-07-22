// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public enum ExecuteStatus
{
    Waiting,

    InProgress,

    Succeed,

    Failed,

    RollbackSucceeded,

    RollbackFailed
}
