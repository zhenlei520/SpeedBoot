// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Expressions;

/// <summary>
/// If the method is a generic method, you need to pass in the execution type of the method
/// </summary>
public static class MethodExpressionUtils
{
    public static Action<object, object[]?> BuildSyncInvokeDelegate(Type instanceType, MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        var lambda = Expression.Lambda<Action<object, object[]?>>(methodCallExpression, instanceParameter, parametersParameter);
        return lambda.Compile();
    }

    public static Func<object, object[]?, Task> BuildTaskInvokeDelegate(Type instanceType, MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        var castMethodCall = Expression.Convert(methodCallExpression, typeof(Task));
        var lambda = Expression.Lambda<Func<object, object[]?, Task>>(castMethodCall, instanceParameter, parametersParameter);
        return lambda.Compile();
    }

    public static Func<object, object[]?, TResponse> BuildSyncInvokeWithResultDelegate<TResponse>(Type instanceType, MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        var lambda = Expression.Lambda<Func<object, object[]?, TResponse>>(methodCallExpression, instanceParameter,
            parametersParameter);
        return lambda.Compile();
    }

    public static Func<object, object[]?, object> BuildSyncInvokeWithResultDelegate(Type instanceType, MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        // Create a lambda expression with correct parameters
        var lambda = Expression.Lambda<Func<object, object[]?, object>>(methodCallExpression, instanceParameter, parametersParameter);
        return lambda.Compile();
    }

    public static Func<object, object[]?, Task<TResponse>> BuildTaskInvokeWithResultDelegate<TResponse>(Type instanceType,
        MethodInfo methodInfo)
    {
        var (methodCallExpression, instanceParameter, parametersParameter) = BuildCore(instanceType, methodInfo);
        var castMethodCall = Expression.Convert(methodCallExpression, methodInfo.ReturnType);
        var lambda = Expression.Lambda<Func<object, object[]?, Task<TResponse>>>(castMethodCall, instanceParameter, parametersParameter);
        return lambda.Compile();
    }

    public static Func<object, object[]?, Task<object>> BuildTaskInvokeWithResultDelegate(Type instanceType, MethodInfo methodInfo)
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
        var lambda = Expression.Lambda<Func<object, object[]?, Task<object>>>(awaitTask, instanceParameter, parametersParameter);

        return lambda.Compile();
    }

    private static (MethodCallExpression MethodCallExpression, ParameterExpression InstanceParameter, ParameterExpression
        parameterExpression)
        BuildCore(Type instanceType, MethodInfo methodInfo)
    {
        // Parameters to executor
        var instanceParameter = Expression.Parameter(typeof(object), "target");
        var parametersParameter = Expression.Parameter(typeof(object?[]), "parameters");

        var paramExpression = methodInfo.GetParameters()
            .Select((param, index) =>
            {
                var indexExpr = Expression.Constant(index);
                var arrayAccessExpr = Expression.ArrayIndex(parametersParameter, indexExpr);
                return Expression.Convert(arrayAccessExpr, param.ParameterType);
            })
            .ToArray();

        // Call method
        var instanceExpression = Expression.Convert(instanceParameter, instanceType);
        var methodCallExpression = Expression.Call(instanceExpression, methodInfo, paramExpression);
        return (methodCallExpression, instanceParameter, parametersParameter);
    }
}
