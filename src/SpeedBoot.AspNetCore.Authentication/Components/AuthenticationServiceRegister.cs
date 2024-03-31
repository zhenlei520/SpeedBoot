// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using SpeedBootIdentity_ServiceCollectionExtensions = Microsoft.Extensions.DependencyInjection.ServiceCollectionExtensions;

namespace SpeedBoot.Authentication.Components;

public class AuthenticationServiceRegister : ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        var identitySection = App.Instance.GetRequiredSingletonService<IConfiguration>(true).GetSection("SpeedBoot:Identity");
        var identityUserFullName = identitySection.GetSection("IdentityUserType").Value;
        var identityUserKeyFullName = identitySection.GetSection("IdentityUserKey").Value;
        var identityUserType = typeof(IdentityUser<Guid>);
        var identityUserKeyType = typeof(Guid);
        if (!identityUserFullName.IsNullOrWhiteSpace())
            identityUserType = Type.GetType(identityUserFullName);
        if (!identityUserKeyFullName.IsNullOrWhiteSpace())
            identityUserKeyType = Type.GetType(identityUserKeyFullName);

        var methodInfo = typeof(SpeedBootIdentity_ServiceCollectionExtensions)
            .GetMethods()
            .FirstOrDefault(m
                => m.Name == nameof(SpeedBootIdentity_ServiceCollectionExtensions.AddSpeedBootIdentity) && m.IsGenericMethod &&
                m.GetGenericArguments().Length == 2 && m.GetParameters().Length == 2);
        SpeedArgumentException.ThrowIfNull(methodInfo);
        methodInfo!.MakeGenericMethod(identityUserType, identityUserKeyType).Invoke(null, new object[] { services, Action });

        void Action(IdentityClaimOptions options)
        {
#if NETCOREAPP3_0_OR_GREATER
            if (!identitySection.Exists())
                return;

            var mappingSection = identitySection.GetSection("ClaimMappings");
            if (!mappingSection.Exists())
                return;

            var mapping = mappingSection.Get<Dictionary<string, string>>();
            foreach (var item in mapping)
            {
                options.Mapping(item.Key, item.Value);
            }
#endif
        }
    }
}
