// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication.Abstractions;

public interface IUserSetter<TIdentityUser, TUserId>
    where TIdentityUser : class, IIdentityUser<TUserId>, new()
    where TUserId : struct, IComparable, IEquatable<TUserId>
{
    IDisposable Change(TIdentityUser? identityUser);
}

public interface IUserSetter<TIdentityUser> : IUserSetter<TIdentityUser, Guid>
    where TIdentityUser : class, IIdentityUser<Guid>, new()
{
}
