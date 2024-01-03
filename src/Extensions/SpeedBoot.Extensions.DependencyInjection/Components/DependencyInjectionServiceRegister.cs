// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.DependencyInjection;

public class DependencyInjectionServiceRegister : ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoInject(App.ApplicationBuilder.Assemblies ?? AppDomain.CurrentDomain.GetAssemblies());
    }
}
