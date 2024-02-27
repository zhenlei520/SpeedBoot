// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection;

public class KeydSingletonService<TService> : KeydService<TService>, IKeydSingletonService<TService>
    where TService : IService
{
    public KeydSingletonService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
