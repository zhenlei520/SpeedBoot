// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Collections.Generic;

internal static class BackgroundJobInvokeBuilder
{
    public static BackgroundJobTaskInvokeDelegate Build(MethodInfo methodInfo, Type targetType)
    {
        // Parameters to executor
        var targetParameter = Expression.Parameter(typeof(object), "target");
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

        var instanceCast = Expression.Convert(targetParameter, targetType);
        var methodCall = Expression.Call(instanceCast, methodInfo, parameters);

        if (methodCall.Type != typeof(Task))
            throw new NotSupportedException($"The return type of the [{methodInfo.Name}] method must be Task");

        var castMethodCall = Expression.Convert(methodCall, typeof(Task));
        var lambda = Expression.Lambda<BackgroundJobTaskInvokeDelegate>(castMethodCall, targetParameter, parametersParameter);
        return lambda.Compile();

    }
}
