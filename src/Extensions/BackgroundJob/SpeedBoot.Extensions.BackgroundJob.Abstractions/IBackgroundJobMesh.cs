// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.BackgroundJob.Abstractions;

public delegate Task BackgroundJobTaskInvokeDelegate(object target, params object?[] parameters);

public interface IBackgroundJobMesh
{
    public Dictionary<Type, BackgroundJobTaskInvokeDelegate> InvokeDelegates { get; }

    public IEnumerable<Type> JobTypes { get; }

    public Type GetJobType(string jobName);

    public Type GetJobArgsType(string jobName);
}
