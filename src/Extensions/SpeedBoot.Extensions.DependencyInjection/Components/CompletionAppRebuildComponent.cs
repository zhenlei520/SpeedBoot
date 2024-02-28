// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public class CompletionAppRebuildComponent : CompletionAppComponentBase
{
    public override int Order { get; } = int.MinValue;
    
    public override void Configure()
    {
        App.Instance.RebuildRootServiceProvider ??= s => s.BuildServiceProvider();
    }
}
