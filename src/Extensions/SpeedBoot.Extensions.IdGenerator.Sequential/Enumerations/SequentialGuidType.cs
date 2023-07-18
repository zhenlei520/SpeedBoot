// ReSharper disable once CheckNamespace

namespace SpeedBoot.Extensions.IdGenerator;

public enum SequentialGuidType
{
    /// <summary>
    /// The GUID should be sequential when formatted using the
    /// Used by MySql and PostgreSql.
    /// </summary>
    SequentialAsString = 0,

    /// <summary>
    /// The GUID should be sequential when formatted using the
    /// Used by Oracle.
    /// </summary>
    SequentialAsBinary = 1,

    /// <summary>
    /// The sequential portion of the GUID should be located at the end
    /// of the Data4 block.
    /// Used by SqlServer.
    /// </summary>
    SequentialAtEnd = 2
}
