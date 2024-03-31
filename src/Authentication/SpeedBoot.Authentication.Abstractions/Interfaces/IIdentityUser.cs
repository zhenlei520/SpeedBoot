// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Authentication.Abstractions;

public interface IIdentityUser<TUserId>
    where TUserId : struct, IComparable, IEquatable<TUserId>
{
    TUserId Id { get; set; }
}
