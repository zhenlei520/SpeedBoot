// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot;

public class CompletionAppStartup: AppStartupBase
{
    private readonly List<Type> _allComponentTypes;

    public override int Order { get; } = int.MinValue;

    public override string Name { get; } = nameof(CompletionAppStartup);

    public CompletionAppStartup(
        IEnumerable<Assembly> assemblies,
        Lazy<ILogger?> loggerLazy, LogLevel? logLevel = null)
        : base(loggerLazy, logLevel)
    {
        _allComponentTypes = GetDerivedClassTypes<ICompletionAppComponent>(assemblies);
    }
    protected override void Load()
    {
        var componentTypes = ServiceComponentStartupHelp.GetComponentTypesByOrdered(_allComponentTypes);
        foreach (var componentInstance in componentTypes.Select(componentType => Activator.CreateInstance(componentType) as ICompletionAppComponent).OrderBy(component => component!.Order))
        {
            SpeedArgumentException.ThrowIfNull(componentInstance);
            componentInstance!.Configure();
        }
    }
}
