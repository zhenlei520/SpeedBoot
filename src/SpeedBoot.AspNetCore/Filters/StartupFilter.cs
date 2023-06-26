// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.


namespace SpeedBoot.AspNetCore.Filters;

public class StartupFilter : IStartupFilter
{

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        return app =>
        {
            AppCore.SetRootServiceProvider(app.ApplicationServices);

            next(app);
        };
    }
}
