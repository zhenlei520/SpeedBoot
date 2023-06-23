// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace
namespace Speed;

public class ServiceCollectionStartup : AppStartupBase
{
    private readonly IServiceCollection _services;
    private readonly List<Type> _serviceComponentTypes;

    public override string Name => "ServiceRegisterComponent";

    public ServiceCollectionStartup(
        IServiceCollection services,
        IEnumerable<Assembly> assemblies,
        ILogger? logger,
        LogLevel? logLevel = null) : base(logger, logLevel)
    {
        _services = services;
        _serviceComponentTypes = assemblies
            .GetTypes(type => type is { IsClass: true, IsGenericType: false, IsAbstract: false } && typeof(IServiceComponent).IsAssignableFrom(type));
    }

    /// <summary>
    /// 还需要调整改为按顺序注册
    /// </summary>
    protected override void Load()
    {
        var componentTypes = ComponentSort();
        foreach (var componentInstance in componentTypes.Select(componentType => Activator.CreateInstance(componentType) as IServiceComponent))
        {
            SpeedArgumentException.ThrowIfNull(componentInstance);
            componentInstance!.ConfigureServices(_services);
        }
    }

    /// <summary>
    /// Ordering service dependencies
    /// </summary>
    /// <returns></returns>
    private List<Type> ComponentSort()
    {
        return _serviceComponentTypes;
    }
}
