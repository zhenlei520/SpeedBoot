namespace SpeedBoot.Extensions.IdGenerator.Sequential.Components;

public class IdGeneratorServiceRegister : ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        SequentialOptions? sequentialOptions = null;
        var speedBootConfiguration = App.Instance.GetConfiguration(true)?.GetSection("SpeedBoot");
        if (speedBootConfiguration != null)
        {
            var idGeneratorConfiguration = speedBootConfiguration.GetSection("IdGenerator");
            sequentialOptions = idGeneratorConfiguration.Get<SequentialOptions>();
            if (sequentialOptions == null && idGeneratorConfiguration != null)
            {
                var databaseTypeStr = speedBootConfiguration["DatabaseType"];
                if (!databaseTypeStr.IsNullOrWhiteSpace())
                {
                    sequentialOptions = new SequentialOptions()
                    {
                        DatabaseType = (DatabaseType)int.Parse(databaseTypeStr)
                    };
                }
            }
        }
        sequentialOptions ??= new SequentialOptions();
        services.AddSequentialIdGenerator(Utils.GetGuidType(sequentialOptions.DatabaseType));
    }
}
