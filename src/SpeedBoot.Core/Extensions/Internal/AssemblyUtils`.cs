// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace System;

internal static partial class AssemblyUtils
{
    public static List<Type> GetServiceComponentTypes(params Assembly[] assemblies)
    {
        return assemblies.GetTypes(type => type is { IsClass: true, IsAbstract: false } && type.IsSubclassOf(typeof(IServiceComponent)));
    }

    public static Assembly[] GetAllAssembly()
        => GetAllAssembly(name => true);

    public static Assembly[] GetAllAssembly(Expression<Func<string, bool>> condition)
    {
        var entryAssemblies = new List<Assembly>();
        var entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly != null)
        {
            var assemblyNames = entryAssembly.GetReferencedAssemblies().ToList();
            entryAssemblies = assemblyNames.Select(Assembly.Load).ToList();
        }

        var externalAssemblies = new List<Assembly>();
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var files = Directory.GetFiles(baseDirectory, "*.dll", SearchOption.AllDirectories)
            .Where(file => file != null && condition.Compile().Invoke(Path.GetFileName(file)))
            .ToList();

        foreach (var assembly in files.Select(file => Assembly.LoadFrom(file!)).Where(assembly => !entryAssemblies.Contains(assembly) && !externalAssemblies.Contains(assembly)))
        {
            externalAssemblies.Add(assembly);
        }

        var assemblies= new List<Assembly>(entryAssemblies);
        assemblies.AddRange(externalAssemblies);
        return assemblies.ToArray();
    }
}
