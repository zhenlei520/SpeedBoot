// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

#if NET6_0_OR_GREATER

namespace SpeedBoot.AspNetCore;

public class GlobalServiceRouteOptions : ServiceRouteOptions
{
    private string _routeTemplate;

    public string RouteTemplate
    {
        get => _routeTemplate;
        set
        {
            SpeedArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(RouteTemplate));
            _routeTemplate = value;
        }
    }

    public bool DisableContainsAppDomainAssemblies { get; set; }

    public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

#if NET7_0_OR_GREATER
    public ServiceLifetime DefaultEndpointFilterServiceLifetime { get; set; }

    public Func<Type, ServiceLifetime?>? EndpointFilterServiceLifetimeFunc { get; set; }
#endif

    /// <summary>
    /// The set of features that need to be supported on the class
    /// </summary>
    public List<Type>? ExtendAttributesWithClass { get; set; }

    public GlobalServiceRouteOptions()
    {
        DisableAutoMapRoute = false;
        _routeTemplate = "/api/v1/[service]/[action]";
        DisablePluralizeServiceName = false;
        GetPrefixes = new List<string> { "Get", "Find" };
        PostPrefixes = new List<string> { "Post", "Add", "Upsert", "Create", "Set" };
        PutPrefixes = new List<string> { "Put", "Update", "Modify" };
        DeletePrefixes = new List<string> { "Delete", "Remove" };
        DisableTrimMethodPrefix = false;
        DisableTrimMethodSuffix = false;
        DisableAutoAppendId = false;
#if NET7_0_OR_GREATER
        DefaultEndpointFilterServiceLifetime = ServiceLifetime.Singleton;
        EndpointFilterServiceLifetimeFunc = null;
#endif
    }
}

#endif
