// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using SpeedBoot.Core;
global using SpeedBoot.Data.Abstractions;
global using SpeedBoot.Ddd.Domain.Entities.Auditing;
global using SpeedBoot.Ddd.Domain.Events;
global using SpeedBoot.Ddd.Domain.Services;
global using SpeedBoot.EventBus.Abstracts;
global using System.Collections.Concurrent;
global using System.Reflection;
