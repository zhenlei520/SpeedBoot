// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

namespace SpeedBoot.AspNetCore.Options;

public class GlobalServiceRouteOptions: ServiceRouteOptions
{
    public bool DisableContainsAppDomainAssemblies { get; set; }

    public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

    public GlobalServiceRouteOptions()
    {
        DisableAutoMapRoute = false;
        Prefix = "api";
        Version = "v1";
        DisablePluralizeServiceName = false;
        GetPrefixes = new List<string> { "Get", "Find" };
        PostPrefixes = new List<string> { "Post", "Add", "Upsert", "Create", "Set" };
        PutPrefixes = new List<string> { "Put", "Update", "Modify" };
        DeletePrefixes = new List<string> { "Delete", "Remove" };
        DisableTrimMethodPrefix = false;
        DisableTrimMethodSuffix = false;
        DisableAutoAppendId = false;
    }
}

#endif
