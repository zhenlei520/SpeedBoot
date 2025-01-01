// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

global using Microsoft.EntityFrameworkCore;
global using System.Collections.Immutable;
global using System.Linq.Expressions;
global using System.Reflection;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using SpeedBoot.Data.Abstractions;
global using Microsoft.Extensions.DependencyInjection;
global using SpeedBoot.Data.EFCore;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Configuration;
global using SpeedBoot;
global using SpeedBoot.Configuration;
global using SpeedBoot.System;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using SpeedBoot.System.Linq;
global using SpeedBoot.Data.EFCore.DataFiltering.Internal;
global using SpeedBoot.Data.Abstractions.DataFiltering;
global using SpeedBoot.System.Collections.Concurrent;
global using SpeedBoot.Data.EFCore.DataFiltering;
global using SpeedBoot.Data.EFCore.Options;
global using Microsoft.EntityFrameworkCore.Metadata;
