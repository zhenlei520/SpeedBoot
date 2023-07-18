// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

internal static class Utils
{
    public static SequentialGuidType GetGuidType(DatabaseType? databaseType)
    {
        switch (databaseType)
        {
            case DatabaseType.MySql:
            case DatabaseType.PostgreSql:
                return SequentialGuidType.SequentialAsString;
            case DatabaseType.Oracle:
                return SequentialGuidType.SequentialAsBinary;
            default:
                return SequentialGuidType.SequentialAtEnd;
        }
    }
}
