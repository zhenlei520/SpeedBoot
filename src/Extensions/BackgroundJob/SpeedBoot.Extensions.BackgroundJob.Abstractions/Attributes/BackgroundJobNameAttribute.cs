// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.BackgroundJob.Abstractions;

[AttributeUsage(AttributeTargets.Class)]
public class BackgroundJobNameAttribute : Attribute
{
    public string Name { get; set; }

    public BackgroundJobNameAttribute(string name)
    {
        SpeedArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    public static string GetName<TJobArgs>()
        => GetName(typeof(TJobArgs));

    public static string GetName(Type jobArgsType)
    {
        SpeedArgumentException.ThrowIfNull(jobArgsType);

        return jobArgsType
                .GetCustomAttributes(true)
                .OfType<BackgroundJobNameAttribute>()
                .FirstOrDefault()
                ?.Name
            ?? jobArgsType.FullName!;
    }
}
