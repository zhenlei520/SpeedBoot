// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.EventBus.Local;

public class LocalEventBusMesh : ILocalEventBusMesh
{
    public Dictionary<Type, DispatchRecord> MeshData { get; set; }

    private readonly Assembly[] _assemblies;
    private readonly ILogger? _logger;

    public LocalEventBusMesh(LocalEventBusOptions localEventBusOptions, ILogger? logger = null)
    {
        _assemblies = localEventBusOptions.GetAssemblies();
        _logger = logger;
        BuildMesh();
    }

    private void BuildMesh()
    {
        var meshData = BuildMeshByAttribute();
        MeshData = MeshSort(meshData);
    }

    private Dictionary<Type, DispatchRecord> BuildMeshByAttribute()
    {
        var data = new Dictionary<Type, DispatchRecord>();
        foreach (var instanceType in _assemblies.GetTypes())
        {
            foreach (var methodInfo in instanceType.GetMethods())
            {
                if (!TryGetEventHandler(instanceType, methodInfo, out var localEventHandlerAttribute))
                    continue;

                data.TryAdd(localEventHandlerAttribute!.EventType, new DispatchRecord());
                data[localEventHandlerAttribute.EventType].AddHandlers(localEventHandlerAttribute);
            }
        }
        return data;
    }

    private bool TryGetEventHandler(Type instanceType, MethodInfo methodInfo, out LocalEventHandlerAttribute? localEventHandlerAttribute)
    {
        try
        {
            localEventHandlerAttribute = methodInfo.GetCustomAttribute<LocalEventHandlerAttribute>();
            if (localEventHandlerAttribute == null)
                return false;

            var parameters = methodInfo.GetParameters().Where(parameter => typeof(IEvent).IsAssignableFrom(parameter.ParameterType))
                .ToList();
            if (parameters.Count != 1)
                throw new ArgumentOutOfRangeException($"[{methodInfo.Name}] only allows one parameter and inherits from Event, other parameters must support getting from DI");

            localEventHandlerAttribute.BuildExpression(instanceType, methodInfo, parameters[0].ParameterType);
            return true;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex,
                "Dispatcher: Failed to get EventBus mesh, instanceType name: [{TypeName}], method: [{MethodName}]",
                instanceType.FullName ?? instanceType.Name,
                methodInfo.Name);
            throw;
        }
    }

    private Dictionary<Type, DispatchRecord> MeshSort(Dictionary<Type, DispatchRecord> meshData)
    {
        // 对 mesh 的 Handlers 按照 Order 冒泡排序、CancelHandlers 按照 Order 冒泡倒排序
        foreach (var mesh in meshData.Values)
        {
            for (var i = 0; i < mesh.Handlers.Count; i++)
            {
                for (var j = 0; j < mesh.Handlers.Count - i - 1; j++)
                {
                    if (mesh.Handlers[j].Order > mesh.Handlers[j + 1].Order)
                    {
                        (mesh.Handlers[j], mesh.Handlers[j + 1]) = (mesh.Handlers[j + 1], mesh.Handlers[j]);
                    }
                }
            }

            for (var i = 0; i < mesh.CancelHandlers.Count; i++)
            {
                for (var j = 0; j < mesh.CancelHandlers.Count - i - 1; j++)
                {
                    if (mesh.CancelHandlers[j].Order < mesh.CancelHandlers[j + 1].Order)
                    {
                        (mesh.CancelHandlers[j], mesh.CancelHandlers[j + 1]) = (mesh.CancelHandlers[j + 1], mesh.CancelHandlers[j]);
                    }
                }
            }
        }
        return meshData;
    }
}
