// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.FreeSql.DataFiltering.Internal;

internal sealed class DisposeAction : IDisposable
{
    private readonly Action _action;

    public DisposeAction(Action action) => _action = action;

    public void Dispose() => _action.Invoke();
}
