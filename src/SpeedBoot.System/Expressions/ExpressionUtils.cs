// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Expressions;

public delegate void SyncInvokeDelegate(object target, params object?[] parameters);

public delegate Task TaskInvokeDelegate(object target, params object?[] parameters);

public delegate TResponse SyncInvokeWithResultDelegate<TResponse>(object target, params object?[] parameters);

public delegate object SyncInvokeWithResultDelegate(object target, params object?[] parameters);

public delegate Task<TResponse> TaskInvokeWithResultDelegate<TResponse>(object target, params object?[] parameters);

public delegate Task<object> TaskInvokeWithResultDelegate(object target, params object?[] parameters);

public static class ExpressionUtils
{
    public static SyncInvokeDelegate BuildSyncInvokeDelegate(Type instanceType, MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        var lambda = Expression.Lambda<SyncInvokeDelegate>(methodCallExpression, instanceParameter, parametersParameter);
        return lambda.Compile();
    }

    public static TaskInvokeDelegate BuildTaskInvokeDelegate(Type instanceType, MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        var castMethodCall = Expression.Convert(methodCallExpression, typeof(Task));
        var lambda = Expression.Lambda<TaskInvokeDelegate>(castMethodCall, instanceParameter, parametersParameter);
        return lambda.Compile();
    }

    public static SyncInvokeWithResultDelegate<TResponse> BuildSyncInvokeWithResultDelegate<TResponse>(Type instanceType,
        MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        var lambda = Expression.Lambda<SyncInvokeWithResultDelegate<TResponse>>(methodCallExpression, instanceParameter,
            parametersParameter);
        return lambda.Compile();
    }

    public static SyncInvokeWithResultDelegate BuildSyncInvokeWithResultDelegate(Type instanceType,
        MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        // Create a lambda expression with correct parameters
        var lambda = Expression.Lambda<SyncInvokeWithResultDelegate>(methodCallExpression, instanceParameter, parametersParameter);
        return lambda.Compile();
    }

    public static TaskInvokeWithResultDelegate<TResponse> BuildTaskInvokeWithResultDelegate<TResponse>(Type instanceType,
        MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        var castMethodCall = Expression.Convert(methodCallExpression, methodInfo.ReturnType);
        var lambda = Expression.Lambda<TaskInvokeWithResultDelegate<TResponse>>(castMethodCall, instanceParameter, parametersParameter);
        return lambda.Compile();
    }

    public static TaskInvokeWithResultDelegate BuildTaskInvokeWithResultDelegate(Type instanceType, MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);

        // Get the return type of the method
        var returnType = methodInfo.ReturnType;

        // If the return type is Task<T>, create a lambda expression to convert the result to object

        var resultType = returnType.GetGenericArguments()[0];

        // Await the task and return its result
        var awaitTask = Expression.Call(typeof(TaskExtensions), nameof(TaskExtensions.AwaitTaskWithResult), [resultType],
            methodCallExpression);
        // Create a lambda expression with correct parameters
        var lambda = Expression.Lambda<TaskInvokeWithResultDelegate>(awaitTask, instanceParameter, parametersParameter);

        return lambda.Compile();
    }

    private static (MethodCallExpression MethodCallExpression, ParameterExpression InstanceParameter, ParameterExpression
        parameterExpression)
        BuildCore(Type instanceType, MethodInfo methodInfo)
    {
        // Parameters to executor
        var instanceParameter = Expression.Parameter(typeof(object), "target");
        var parametersParameter = Expression.Parameter(typeof(object?[]), "parameters");

        // Build parameter list
        var parameters = new List<Expression>();
        var paramInfos = methodInfo.GetParameters();
        for (var i = 0; i < paramInfos.Length; i++)
        {
            var paramInfo = paramInfos[i];
            var valueObj = Expression.ArrayIndex(parametersParameter, Expression.Constant(i));
            var valueCast = Expression.Convert(valueObj, paramInfo.ParameterType);

            // valueCast is "(Ti) parameters[i]"
            parameters.Add(valueCast);
        }

        // Call method
        var instanceCast = Expression.Convert(instanceParameter, instanceType);
        var methodCallExpression = Expression.Call(instanceCast, methodInfo, parameters);
        return (methodCallExpression, instanceParameter, parametersParameter);
    }
}
