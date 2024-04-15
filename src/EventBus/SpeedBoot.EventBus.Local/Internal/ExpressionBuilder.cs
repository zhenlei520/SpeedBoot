// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

internal delegate Task TaskInvokeDelegate(object target, params object?[] parameters);

internal delegate Task<object> TaskInvokeDelegate2(object target, params object?[] parameters);

internal delegate void VoidInvokeDelegate(object target, object?[] parameters);

internal delegate object VoidInvokeDelegate2(object target, object?[] parameters);

public class ExpressionBuilder
{
    internal static TaskInvokeDelegate Build(MethodInfo methodInfo, Type targetType)
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

        // Call method
        var instanceCast = Expression.Convert(targetParameter, targetType);
        var methodCall = Expression.Call(instanceCast, methodInfo, parameters);

        // methodCall is "((Ttarget) target) method((T0) parameters[0], (T1) parameters[1], ...)"
        // Create function
        if (methodCall.Type == typeof(void))
        {
            var lambda = Expression.Lambda<VoidInvokeDelegate>(methodCall, targetParameter, parametersParameter);
            var voidExecutor = lambda.Compile();
            return delegate (object target, object?[] parameters)
            {
                voidExecutor(target, parameters);
                return Task.CompletedTask;
            };
        }
        if (methodCall.Type == typeof(Task))
        {
            // must coerce methodCall to match ActionExecutor signature
            var castMethodCall = Expression.Convert(methodCall, typeof(Task));
            var lambda = Expression.Lambda<TaskInvokeDelegate>(castMethodCall, targetParameter, parametersParameter);
            return lambda.Compile();
        }
        throw new NotSupportedException($"The return type of the [{methodInfo.Name}] method must be Task or void");
    }

    internal static TaskInvokeDelegate2 Build2(MethodInfo methodInfo, Type targetType)
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

        // Call method
        var instanceCast = Expression.Convert(targetParameter, targetType);
        var methodCall = Expression.Call(instanceCast, methodInfo, parameters);

        // methodCall is "((Ttarget) target) method((T0) parameters[0], (T1) parameters[1], ...)"
        // Create function
        if (methodCall.Type == typeof(void))
        {
            var lambda = Expression.Lambda<VoidInvokeDelegate2>(methodCall, targetParameter, parametersParameter);
            var voidExecutor = lambda.Compile();
            return delegate (object target, object?[] parameters)
            {
                return Task.FromResult(voidExecutor(target, parameters));
            };
        }
        if ( typeof(Task<>).IsAssignableFrom( methodCall.Type.GetGenericTypeDefinition()))
        {
            // must coerce methodCall to match ActionExecutor signature
            var castMethodCall = Expression.Convert(methodCall, typeof(Task));
            var lambda = Expression.Lambda<TaskInvokeDelegate2>(castMethodCall, targetParameter, parametersParameter);
            return lambda.Compile();
        }
        throw new NotSupportedException($"The return type of the [{methodInfo.Name}] method must be Task or void");
    }
}
