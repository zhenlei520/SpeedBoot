// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

[assembly: HostingStartup(typeof(SpeedHostingStartup))]

namespace SpeedBoot.AspNetCore;

public class SpeedHostingStartup : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        builder.ConfigureServices((webHostBuilderContext, services) =>
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHostedService<GenericHostedService>();

            services.AddConfiguration(webHostBuilderContext.Configuration);
            var speedBootApplicationExternal = services.AddSpeed(speedOptions =>
            {
                var includeAssemblyRules = webHostBuilderContext.Configuration.GetSection("SpeedBoot")
                    .GetValue<List<string>>("IncludeAssemblyRules");
                if (includeAssemblyRules.IsAny())
                {
                    speedOptions.IncludeAssemblyRules = includeAssemblyRules;
                }
                else
                {
                    var assemblyName = webHostBuilderContext.Configuration["SpeedBoot:AssemblyName"];
                    if (assemblyName != null) speedOptions.IncludeAssemblyRules = new List<string>() { assemblyName };
                }
                var excludeAssemblyRules = webHostBuilderContext.Configuration.GetSection("SpeedBoot").GetValue<List<string>>("ExcludeAssemblyRules");
                if (excludeAssemblyRules.IsAny())
                {
                    speedOptions.ExcludeAssemblyRules = excludeAssemblyRules;
                }

                var defaultExcludeAssemblyRules = webHostBuilderContext.Configuration.GetSection("SpeedBoot").GetValue<List<string>>("DefaultExcludeAssemblyRules");
                if (defaultExcludeAssemblyRules.IsAny())
                {
                    speedOptions.DefaultExcludeAssemblyRules = defaultExcludeAssemblyRules;
                }
                speedOptions.Environment = webHostBuilderContext.HostingEnvironment.EnvironmentName;
            });
            speedBootApplicationExternal.Initialize();
        });
    }
}
