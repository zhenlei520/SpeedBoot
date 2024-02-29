// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot;

public class ServiceRegisterStartup : AppStartupBase
{
    private readonly IServiceCollection _services;
    private readonly List<Type> _allServiceComponentTypes;

    public override string Name { get; } = nameof(ServiceRegisterStartup);

    public ServiceRegisterStartup(
        IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        Lazy<ILogger?> loggerLazy,
        LogLevel? logLevel = null) : base(loggerLazy, logLevel)
    {
        _services = services;
        _allServiceComponentTypes = GetDerivedClassTypes<IServiceComponent>(assemblies);
    }

    protected override void Load()
    {
        var componentTypes = ServiceComponentStartupHelp.GetComponentTypesByOrdered(_allServiceComponentTypes);
        foreach (var componentInstance in componentTypes.Select(componentType =>
        {
            var constructorInfo = componentType.GetConstructors().OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
            SpeedArgumentException.ThrowIfNull(constructorInfo);
            var parameterTypes = constructorInfo!.GetParameters().Select(parameter => parameter.ParameterType).ToArray();
            if (parameterTypes.Length > 0)
            {
                return Activator.CreateInstance(componentType, parameterTypes.Select(type => App.Instance.GetRequiredSingletonService(type, true)).ToArray()) as IServiceComponent;
            }
            return Activator.CreateInstance(componentType) as IServiceComponent;
        }).OrderBy(component => component!.Order))
        {
            SpeedArgumentException.ThrowIfNull(componentInstance);
            componentInstance!.ConfigureServices(_services);
        }
    }
}
