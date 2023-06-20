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
        _serviceComponentTypes = assemblies.GetTypes(type =>
            type is { IsClass: true, IsGenericType: false, IsAbstract: false } && typeof(IServiceComponent).IsAssignableFrom(type));
    }

    /// <summary>
    /// 还需要调整改为按顺序注册
    /// </summary>
    protected override void Load()
    {
        // var typesByDepend = new List<Type>();
        // _serviceComponentTypes.ForEach(type =>
        // {
        //
        // });
        //
        // var componentInstance = Activator.CreateInstance(type) as IServiceComponent;
        // SpeedArgumentException.ThrowIfNull(componentInstance);
        //
        // if (componentInstance is ServiceComponentBase serviceComponentBase)
        // {
        //     serviceComponentBase.DependComponentTypes
        // }
        // componentInstance.ConfigureServices(_services);
    }
}
