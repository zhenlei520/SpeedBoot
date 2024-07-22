// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

internal static class Utils
{
    public static SequentialGuidType GetGuidType(DatabaseType? databaseType)
    {
        return databaseType switch
        {
            DatabaseType.MySql or DatabaseType.PostgreSql => SequentialGuidType.SequentialAsString,
            DatabaseType.Oracle => SequentialGuidType.SequentialAsBinary,
            _ => SequentialGuidType.SequentialAtEnd
        };
    }
}
