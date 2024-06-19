// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Extensions.Serialization.SystemTextJson.Components;

public class SystemTextJsonServiceRegister: ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSystemTextJson(new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
    }
}
