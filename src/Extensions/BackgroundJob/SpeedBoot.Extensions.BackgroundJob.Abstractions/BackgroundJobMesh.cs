// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.BackgroundJob.Abstractions;

public class BackgroundJobMesh : IBackgroundJobMesh
{
    private readonly IEnumerable<Type> _jobTypes;
    private readonly Dictionary<Type, BackgroundJobTaskInvokeDelegate> _delegateData;
    private readonly Dictionary<string, Type> _jobNameAndJobTypeData;
    private readonly Dictionary<string, Type> _jobNameAndJobArgsTypeData;

    public BackgroundJobMesh(Assembly[] assemblies)
    {
        _jobTypes = assemblies.GetJobTypes(typeof(IBackgroundJob<>));
        Initialize();
    }

    private void Initialize()
    {
        foreach (var jobType in _jobTypes)
        {
            var jobArgsType = jobType.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBackgroundJob<>))!.GetGenericArguments()[0];
            var methodInfo = jobType.GetMethod(nameof(IBackgroundJob<object>.ExecuteAsync), new Type[] { jobArgsType });
            SpeedArgumentException.ThrowIfNull(methodInfo);
            var taskInvokeDelegate = BackgroundJobInvokeBuilder.Build(methodInfo!, jobType);
            _delegateData.TryAdd(jobType, taskInvokeDelegate);
            var jobName = BackgroundJobNameAttribute.GetName(jobArgsType);
            _jobNameAndJobArgsTypeData.TryAdd(jobName, jobArgsType);
            _jobNameAndJobTypeData.TryAdd(jobName, jobType);
        }
    }

    public Dictionary<Type, BackgroundJobTaskInvokeDelegate> InvokeDelegates => _delegateData;

    public IEnumerable<Type> JobTypes => _jobTypes;

    public Type GetJobType(string jobName)
    {
        if (_jobNameAndJobTypeData.TryGetValue(jobName, out var type))
        {
            return type;
        }

        throw new NotSupportedException(jobName);
    }

    public Type GetJobArgsType(string jobName)
    {
        if (_jobNameAndJobArgsTypeData.TryGetValue(jobName, out var type))
        {
            return type;
        }

        throw new NotSupportedException(jobName);
    }
}
