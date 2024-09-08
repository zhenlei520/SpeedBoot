// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.BackgroundJob.Abstractions;

public class JobContext
{
    public IServiceProvider ServiceProvider { get; private set; }

    /// <summary>
    /// Job type collection
    /// </summary>
    public List<Type> Types { get; set; }

    /// <summary>
    /// Job parameters
    /// </summary>
    public object? Args { get; set; }

    public JobContext(IServiceProvider serviceProvider, List<Type> types, object? args)
    {
        ServiceProvider = serviceProvider;
        Types = types;
        Args = args;
    }
}
