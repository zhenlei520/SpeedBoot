// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Authentication.Tests;

[TestClass]
public class UserContextTest
{
    [TestMethod]
    public void Test()
    {
        var services = new ServiceCollection();
        services.AddSpeedBootIdentity<UserContextItem, long>(options =>
            options.Mapping(nameof(UserContextItem.Name), "name").Mapping(nameof(UserContextItem.Gender), "gender")
        );
        services.AddHttpContextAccessor();
        var serviceProvider = services.BuildServiceProvider();
        var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        httpContextAccessor.HttpContext = new DefaultHttpContext()
        {
            User = new ClaimsPrincipal(new List<ClaimsIdentity>()
            {
                new(new List<Claim>()
                {
                    new(ClaimTypes.NameIdentifier, "1"),
                    new("name", "jack"),
                    new("gender", "0"),
                })
            })
        };
        var userContext = serviceProvider.GetRequiredService<IUserContext<UserContextItem, long>>();
        var user = userContext.User;
        Assert.IsTrue(user is { Id: 1, Name: "jack", Gender: false });
        var userSetter= serviceProvider.GetRequiredService<IUserSetter<UserContextItem, long>>();
        using (userSetter.Change(new UserContextItem()
               {
                   Id = 2,
                   Name = "tom",
                   Gender = true
               }))
        {
            userContext = serviceProvider.GetRequiredService<IUserContext<UserContextItem, long>>();
            user = userContext.User;
            Assert.IsTrue(user is { Id: 2, Name: "tom", Gender: true });
        }
        var s = typeof(IUserContext<IdentityUser<Guid>>);
        userContext = serviceProvider.GetRequiredService<IUserContext<UserContextItem, long>>();
        user = userContext.User;
        Assert.IsTrue(user is { Id: 1, Name: "jack", Gender: false });
    }

    public class UserContextItem : SpeedBoot.Authentication.Abstractions.IdentityUser<long>
    {
        public string Name { get; set; }

        public bool? Gender { get; set; }
    }
}
