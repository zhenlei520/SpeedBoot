﻿// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.System.Threading.Tasks;

public static class TaskExtensions
{
    /// <summary>
    /// Asynchronous method to synchronous method
    /// </summary>
    /// <param name="task"></param>
    /// <param name="continueOnCapturedContext">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
    /// <returns></returns>
    public static void ToSync(this Task task, bool continueOnCapturedContext = false)
    {
        task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Asynchronous method to synchronous method
    /// </summary>
    /// <param name="task"></param>
    /// <param name="continueOnCapturedContext">true to attempt to marshal the continuation back to the original context captured; otherwise, false.</param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    public static TResult ToSync<TResult>(this Task<TResult> task, bool continueOnCapturedContext = false)
    {
        return task.ConfigureAwait(continueOnCapturedContext).GetAwaiter().GetResult();
    }

    internal static async Task<object?> AwaitTaskWithResult<T>(this Task<T> task)
    {
        var result = await task;
        return result;
    }
}
