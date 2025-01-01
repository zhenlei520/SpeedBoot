// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.Data.EFCore.Tests.Infrastructure;

public class TestDbContext : SpeedDbContext
{
    public DbSet<User> User { get; set; }

    public DbSet<Person> Person { get; set; }

    public DbSet<Tag> Tag { get; set; }

    public TestDbContext(DbContextOptions<TestDbContext> options, IServiceProvider serviceProvider)
        : base(options, serviceProvider)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasKey(e => new { e.Id, e.Name });
        base.OnModelCreating(modelBuilder);
    }
}
