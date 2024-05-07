// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.System.Tests;

[TestClass]
public class TypeExtensionsTest
{
    [TestMethod]
    public void TestIsInherit()
    {

    }
}

public interface IRepository<TEntity> where TEntity : class
{
}

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public Repository()
    {

    }

    public Repository(string name) : this()
    {

    }

    public Repository(string name, int id)
        : this()
    {

    }

    public Repository(string name, int id, bool age)
        : this(name, id)
    {

    }

    public Repository(string name, int id, bool age, DateTime time)
        : this(name, id, age)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly)
        : this(name, id, age, time)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly)
        : this(name, id, age, time, dateOnly)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long)
        : this(name, id, age, time, dateOnly, timeOnly)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long, ushort @ushort)
        : this(name, id, age, time, dateOnly, timeOnly, @long)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long, ushort @ushort, ulong @ulong)
        : this(name, id, age, time, dateOnly, timeOnly, @long, @ushort)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long, ushort @ushort, ulong @ulong, uint @uint)
        : this(name, id, age, time, dateOnly, timeOnly, @long, @ushort, @ulong)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long, ushort @ushort, ulong @ulong, uint @uint, UInt16 @uint16, byte @byte)
        : this(name, id, age, time, dateOnly, timeOnly, @long, @ushort, @ulong, @uint)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long, ushort @ushort, ulong @ulong, uint @uint, UInt16 @uint16, byte @byte, UInt32 @uint32)
        : this(name, id, age, time, dateOnly, timeOnly, @long, @ushort, @ulong, @uint, @uint16, @byte)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long, ushort @ushort, ulong @ulong, uint @uint, UInt16 @uint16, byte @byte, UInt32 @uint32, float @float)
        : this(name, id, age, time, dateOnly, timeOnly, @long, @ushort, @ulong, @uint, @uint16, @byte, @uint32)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long, ushort @ushort, ulong @ulong, uint @uint, UInt16 @uint16, byte @byte, UInt32 @uint32, float @float, double @double)
        : this(name, id, age, time, dateOnly, timeOnly, @long, @ushort, @ulong, @uint, @uint16, @byte, @uint32, @float)
    {

    }

    public Repository(string name, int id, bool age, DateTime time, DateOnly dateOnly, TimeOnly timeOnly, long @long, ushort @ushort, ulong @ulong, uint @uint, UInt16 @uint16, byte @byte, UInt32 @uint32, float @float, double @double, decimal @decimal)
        : this(name, id, age, time, dateOnly, timeOnly, @long, @ushort, @ulong, @uint, @uint16, @byte, @uint32, @float, @double)
    {

    }
}
