// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// ReSharper disable once CheckNamespace

namespace SpeedBoot.AspNetCore;

public class GenericHostedService : IHostedService
{
    public GenericHostedService(IHost host) => host.Services.GetRequiredService<SpeedBootApplicationBuilder>().SetServiceProvider(host.Services);

    public Task StartAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
