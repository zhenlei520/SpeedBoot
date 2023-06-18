// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Speed;

public class ComponentContext
{
    // public IServiceCollection Services { get; set; } = new ServiceCollection();

    public Type ComponentType { get; set; }

    public List<Type> DependComponentTypes { get; set; } = new();

    /// <summary>
    /// does not depend on any components
    /// </summary>
    public bool IsRoot => DependComponentTypes.Count == 0;
}
