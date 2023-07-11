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
                var includeAssemblyRules = GetRules(webHostBuilderContext.Configuration, "SpeedBoot:IncludeAssemblyRules");
                if (includeAssemblyRules != null)
                {
                    speedOptions.IncludeAssemblyRules = includeAssemblyRules;
                }
                else
                {
                    var assemblyName = webHostBuilderContext.Configuration["SpeedBoot:AssemblyName"];
                    if (assemblyName != null) speedOptions.IncludeAssemblyRules = new List<string>() { assemblyName };
                }

                var excludeAssemblyRules = GetRules(webHostBuilderContext.Configuration, "SpeedBoot:ExcludeAssemblyRules");
                if (excludeAssemblyRules != null)
                {
                    speedOptions.ExcludeAssemblyRules = excludeAssemblyRules;
                }

                var defaultExcludeAssemblyRules = GetRules(webHostBuilderContext.Configuration, "SpeedBoot:DefaultExcludeAssemblyRules");
                if (defaultExcludeAssemblyRules != null)
                {
                    speedOptions.DefaultExcludeAssemblyRules = defaultExcludeAssemblyRules;
                }

                speedOptions.Environment = webHostBuilderContext.HostingEnvironment.EnvironmentName;
            });
            speedBootApplicationExternal.Initialize();
        });

        List<string>? GetRules(IConfiguration configuration, string sectionName)
        {
            var rules = new List<string>();
            configuration.GetSection(sectionName).Bind(rules);
            if (rules.IsAny())
            {
                return rules;
            }
            var rulesStr = configuration[sectionName];
            return !rulesStr.IsNullOrWhiteSpace() ? rulesStr.Split(';').ToList() : null;
        }
    }
}
