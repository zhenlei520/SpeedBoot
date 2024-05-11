// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Expressions;

public static class InstanceExpressionUtils
{
    public static Func<object[]?, object> BuildCreateInstanceDelegate(ConstructorInfo constructorInfo)
        => BuildCreateInstanceDelegate<object>(constructorInfo);

    public static Func<object[]?, TInstance> BuildCreateInstanceDelegate<TInstance>(ConstructorInfo constructorInfo)
        where TInstance : class
    {
        var parametersParameter = Expression.Parameter(typeof(object?[]), "parameters");
        var paramExpression = constructorInfo.GetParameters()
            .Select((param, index) =>
            {
                var indexExpr = Expression.Constant(index);
                var arrayAccessExpr = Expression.ArrayIndex(parametersParameter, indexExpr);
                return Expression.Convert(arrayAccessExpr, param.ParameterType);
            })
            .ToArray();

        var newExpression = Expression.New(constructorInfo, paramExpression);
        var lambdaExpression = Expression.Lambda<Func<object[], TInstance>>(newExpression, parametersParameter);
        return lambdaExpression.Compile()!;
    }

    public static Func<object> BuildCreateInstanceDelegate(Type instanceType,
        BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var constructor = instanceType.GetRequestConstructor(bindingAttr,Array.Empty<Type>());
        var newExpression = Expression.New(constructor);
        var lambdaExpression = Expression.Lambda<Func<object>>(newExpression, null);
        return lambdaExpression.Compile();
    }

    public static Func<TParameter, object> BuildCreateInstanceDelegate<TParameter>(Type instanceType,
        BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterType = typeof(TParameter);
        var parameterExpression = Expression.Parameter(parameterType);
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterType);
        var newExpression = Expression.New(constructor, parameterExpression);
        var lambdaExpression = Expression.Lambda<Func<TParameter, object>>(newExpression, parameterExpression);
        return lambdaExpression.Compile();
    }

    public static Func<TParameter1, TParameter2, object> BuildCreateInstanceDelegate<TParameter1, TParameter2>(Type instanceType,
        BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[] { typeof(TParameter1), typeof(TParameter2) };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression = Expression.Lambda<Func<TParameter1, TParameter2, object>>(newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static Func<TParameter1, TParameter2, TParameter3, object> BuildCreateInstanceDelegate<TParameter1, TParameter2, TParameter3>(
        Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[] { typeof(TParameter1), typeof(TParameter2), typeof(TParameter3) };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression = Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, object>>(newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static Func<TParameter1, TParameter2, TParameter3, TParameter4, object> BuildCreateInstanceDelegate<TParameter1, TParameter2,
        TParameter3, TParameter4>(Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[] { typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4) };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, object>>(newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, object> BuildCreateInstanceDelegate<TParameter1,
        TParameter2, TParameter3, TParameter4, TParameter5>(Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
            { typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5) };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, object>>(newExpression,
                parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, object> BuildCreateInstanceDelegate<
        TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6>(Type instanceType,
        BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, object>>(newExpression,
                parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, object>
        BuildCreateInstanceDelegate<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7>(
            Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression.Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, object>>(
                newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, object>
        BuildCreateInstanceDelegate<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8>(
            Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<
                    Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, object>>(
                    newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static
        Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9, object>
        BuildCreateInstanceDelegate<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
            TParameter9>(Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8), typeof(TParameter9)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
                    TParameter9, object>>(newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static
        Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9,
            TParameter10, object> BuildCreateInstanceDelegate<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6,
            TParameter7, TParameter8, TParameter9, TParameter10>(Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8), typeof(TParameter9), typeof(TParameter10)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
                    TParameter9, TParameter10, object>>(newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static
        Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9,
            TParameter10, TParameter11, object> BuildCreateInstanceDelegate<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5,
            TParameter6, TParameter7, TParameter8, TParameter9, TParameter10, TParameter11>(Type instanceType,
            BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8), typeof(TParameter9), typeof(TParameter10), typeof(TParameter11)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
                    TParameter9, TParameter10, TParameter11, object>>(newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static
        Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9,
            TParameter10, TParameter11, TParameter12, object> BuildCreateInstanceDelegate<TParameter1, TParameter2, TParameter3,
            TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9, TParameter10, TParameter11, TParameter12>(
            Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8), typeof(TParameter9), typeof(TParameter10), typeof(TParameter11), typeof(TParameter12)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
                    TParameter9, TParameter10, TParameter11, TParameter12, object>>(newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static
        Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9,
            TParameter10, TParameter11, TParameter12, TParameter13, object> BuildCreateInstanceDelegate<TParameter1, TParameter2,
            TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9, TParameter10, TParameter11,
            TParameter12, TParameter13>(Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8), typeof(TParameter9), typeof(TParameter10), typeof(TParameter11), typeof(TParameter12),
            typeof(TParameter13)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
                    TParameter9, TParameter10, TParameter11, TParameter12, TParameter13, object>>(newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static
        Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9,
            TParameter10, TParameter11, TParameter12, TParameter13, TParameter14, object> BuildCreateInstanceDelegate<TParameter1,
            TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9, TParameter10,
            TParameter11, TParameter12, TParameter13, TParameter14>(Type instanceType,
            BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8), typeof(TParameter9), typeof(TParameter10), typeof(TParameter11), typeof(TParameter12),
            typeof(TParameter13), typeof(TParameter14)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
                    TParameter9, TParameter10, TParameter11, TParameter12, TParameter13, TParameter14, object>>(newExpression,
                    parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static
        Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9,
            TParameter10, TParameter11, TParameter12, TParameter13, TParameter14, TParameter15, object> BuildCreateInstanceDelegate<
            TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9,
            TParameter10, TParameter11, TParameter12, TParameter13, TParameter14, TParameter15>(Type instanceType,
            BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8), typeof(TParameter9), typeof(TParameter10), typeof(TParameter11), typeof(TParameter12),
            typeof(TParameter13), typeof(TParameter14), typeof(TParameter15)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
                    TParameter9, TParameter10, TParameter11, TParameter12, TParameter13, TParameter14, TParameter15, object>>(newExpression,
                    parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static
        Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8, TParameter9,
            TParameter10, TParameter11, TParameter12, TParameter13, TParameter14, TParameter15, TParameter16, object>
        BuildCreateInstanceDelegate<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
            TParameter9, TParameter10, TParameter11, TParameter12, TParameter13, TParameter14, TParameter15, TParameter16>(
            Type instanceType, BindingFlags bindingAttr = Constant.DefaultBindingFlags)
    {
        var parameterTypes = new[]
        {
            typeof(TParameter1), typeof(TParameter2), typeof(TParameter3), typeof(TParameter4), typeof(TParameter5), typeof(TParameter6),
            typeof(TParameter7), typeof(TParameter8), typeof(TParameter9), typeof(TParameter10), typeof(TParameter11), typeof(TParameter12),
            typeof(TParameter13), typeof(TParameter14), typeof(TParameter15), typeof(TParameter16)
        };
        var parameterExpressions = parameterTypes
            .Select(Expression.Parameter)
            .ToArray();
        var constructor = instanceType.GetRequestConstructor(bindingAttr, parameterTypes);
        var newExpression = Expression.New(constructor, parameterExpressions);
        var lambdaExpression =
            Expression
                .Lambda<Func<TParameter1, TParameter2, TParameter3, TParameter4, TParameter5, TParameter6, TParameter7, TParameter8,
                    TParameter9, TParameter10, TParameter11, TParameter12, TParameter13, TParameter14, TParameter15, TParameter16, object>>(
                    newExpression, parameterExpressions);
        return lambdaExpression.Compile();
    }

    public static Action<TInstance, object> SetPropertyValue<TInstance>(string propertyName)
    {
        return SetPropertyValue<TInstance>(propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    public static Action<TInstance, object> SetPropertyValue<TInstance>(string propertyName, BindingFlags bindingFlags)
    {
        return SetPropertyValue<TInstance>(typeof(TInstance).GetProperty(propertyName, bindingFlags)!);
    }

    public static Action<TInstance, object> SetPropertyValue<TInstance>(PropertyInfo propertyInfo)
    {
        var targetObjectParamExpression = Expression.Parameter(typeof(TInstance), "targetObject");
        var valueParamExpression = Expression.Parameter(typeof(object), "value");

        var valueConverted = Expression.Convert(valueParamExpression, propertyInfo.PropertyType);

        var propertyExpression = Expression.Property(targetObjectParamExpression, propertyInfo);
        var assign = Expression.Assign(propertyExpression, valueConverted);
        return Expression.Lambda<Action<TInstance, object>>(assign, targetObjectParamExpression, valueParamExpression).Compile();
    }

    public static Action<object, object> SetPropertyValue(Type instanceType, string propertyName)
    {
        return SetPropertyValue(instanceType, propertyName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    public static Action<object, object> SetPropertyValue(Type instanceType, string propertyName, BindingFlags bindingFlags)
    {
        return SetPropertyValue(instanceType, instanceType.GetProperty(propertyName, bindingFlags)!);
    }

    public static Action<object, object> SetPropertyValue(Type instanceType, PropertyInfo propertyInfo)
    {
        var targetObjectParamExpression = Expression.Parameter(typeof(object), "targetObject");
        var valueParamExpression = Expression.Parameter(typeof(object), "value");

        var targetObjectConverted = Expression.Convert(targetObjectParamExpression, instanceType);
        var valueConverted = Expression.Convert(valueParamExpression, propertyInfo.PropertyType);

        var propertyExpression = Expression.Property(targetObjectConverted, propertyInfo);
        var assign = Expression.Assign(propertyExpression, valueConverted);
        return Expression.Lambda<Action<object, object>>(assign, targetObjectParamExpression, valueParamExpression).Compile();
    }
}
