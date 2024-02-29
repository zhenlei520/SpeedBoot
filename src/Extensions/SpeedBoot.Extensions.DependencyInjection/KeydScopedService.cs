// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public class KeydScopedService<TService> : KeydService<TService>, IKeydScopedService<TService>
    where TService : IService
{
    public KeydScopedService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
