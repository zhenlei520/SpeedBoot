// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication.Abstractions;

public interface IUserContext<TIdentityUser, TUserId>
    where TIdentityUser : class, IIdentityUser<TUserId>, new()
    where TUserId : struct, IComparable, IEquatable<TUserId>
{
    bool IsAuthenticated { get; }

    TUserId? UserId { get; }

    TIdentityUser? User { get; }
}

public interface IUserContext<TIdentityUser> : IUserContext<TIdentityUser, Guid>
    where TIdentityUser : class, IIdentityUser<Guid>, new()
{

}
