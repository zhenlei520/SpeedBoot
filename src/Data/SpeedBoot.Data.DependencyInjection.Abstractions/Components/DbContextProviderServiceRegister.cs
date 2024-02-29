// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.Data.DependencyInjection.Abstractions;

/// <summary>
/// DbContext Provider Registrar
/// </summary>
public class DbContextProviderServiceRegister : ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
        => services.AddSpeedDbContextCore();
}
