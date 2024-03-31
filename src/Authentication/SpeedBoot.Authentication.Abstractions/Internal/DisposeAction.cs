// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication.Abstractions;

internal sealed class DisposeAction(Action action) : IDisposable
{
    public void Dispose() => action();
}
