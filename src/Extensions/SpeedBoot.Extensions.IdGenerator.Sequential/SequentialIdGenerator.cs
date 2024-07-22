namespace SpeedBoot.Extensions.IdGenerator.Sequential;

public class SequentialIdGenerator : IIdGenerator
{
    private static readonly RandomNumberGenerator RandomGenerator = RandomNumberGenerator.Create();

    private readonly SequentialGuidType _guidType;

    public string Key { get; private set; }

    protected SequentialIdGenerator(string key)
    {
        Key = key;
    }

    public SequentialIdGenerator(SequentialGuidType guidType, string key) : this(key)
        => _guidType = guidType;

    public Guid Create()
    {
        byte[] randomBytes = GetRandomBytes();
        byte[] timestampBytes = GetTimestampBytes();
        byte[] guidBytes = GetGuidBytes(_guidType, timestampBytes, randomBytes);
        return new Guid(guidBytes);
    }

    private byte[] GetTimestampBytes()
    {
        long timestamp = DateTime.UtcNow.Ticks / 10000L;
        byte[] timestampBytes = BitConverter.GetBytes(timestamp);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(timestampBytes);
        }

        return timestampBytes;
    }

    private byte[] GetRandomBytes()
    {
        byte[] randomBytes = new byte[10];
        RandomGenerator.GetBytes(randomBytes);
        return randomBytes;
    }

    private byte[] GetGuidBytes(SequentialGuidType sequentialGuidType, byte[] timestampBytes, byte[] randomBytes)
    {
        byte[] guidBytes = new byte[16];
        switch (sequentialGuidType)
        {
            case SequentialGuidType.SequentialAtEnd:

                Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                break;

            case SequentialGuidType.SequentialAsString:
            case SequentialGuidType.SequentialAsBinary:

                Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                if (sequentialGuidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                {
                    Array.Reverse(guidBytes, 0, 4);
                    Array.Reverse(guidBytes, 4, 2);
                }

                break;

            default:
                throw new NotSupportedException($"unsupported {sequentialGuidType}");
        }

        return guidBytes;
    }
}
