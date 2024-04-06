// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using SpeedBoot.AspNetCore.Tests.Infrastructure.BLL;

namespace SpeedBoot.AspNetCore.Tests.Services;

public class UserService : ServiceBase
{
    [HttpPost]
    public string GenerateToken()
    {
        return CreateToken(new Claim[]
        {
            new Claim("sub", "1"),
            new Claim("name", "jack")
        });
    }

    public string GetName(IUserContext<CustomUser, int> userContext)
    {
        return userContext.User?.Name ?? "空";
    }

    /// <summary>
    /// api/v1/users/username
    /// </summary>
    /// <param name="userBll"></param>
    /// <returns></returns>
    public string GetUserName(UserBLL userBll)
    {
        return userBll.GetUserName();
    }

    /// <summary>
    /// api/v1/users/username2
    /// </summary>
    /// <param name="userDal"></param>
    /// <returns></returns>
    public string GetUserName2(IUserDAL userDal)
    {
        return userDal.GetUserName();
    }

    private string CreateToken(Claim[] claims, TimeSpan? timeout = null)
    {
        DateTime notBefore = DateTime.UtcNow;
        DateTime expires = notBefore.Add(timeout ?? TimeSpan.FromDays(7));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345678912345678912345678912345"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            "SpeedBoot",
            "SpeedBoot",
            claims,
            notBefore,
            expires,
            credentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
