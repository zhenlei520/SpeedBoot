// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class LocalEventExecuteContext
{
    public int Counter { get; set; }

    public ExecuteStatus Status { get; private set; }

    public Exception? Exception { get; set; }

    public void Execute()
    {
        UpdateStatus(ExecuteStatus.InProgress);
    }

    public void Succeed()
    {
        UpdateStatus(ExecuteStatus.Succeed);
    }

    public void Failed()
    {
        UpdateStatus(ExecuteStatus.Failed);
    }

    public void RollbackFailed()
    {
        UpdateStatus(ExecuteStatus.RollbackFailed);
    }

    public void RollbackRollbackSucceeded()
    {
        UpdateStatus(ExecuteStatus.RollbackSucceeded);
    }

    private void UpdateStatus(ExecuteStatus executeStatus)
    {
        Status = executeStatus;
    }

    public void Reset()
    {
        UpdateStatus(ExecuteStatus.Waiting);
        Counter = 0;
        Exception = null;
    }
}
