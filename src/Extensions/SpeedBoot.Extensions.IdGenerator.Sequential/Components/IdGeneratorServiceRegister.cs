﻿namespace SpeedBoot.Extensions.IdGenerator.Sequential.Components;

public class IdGeneratorServiceRegister : ServiceRegisterComponentBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        DatabaseType? databaseType = null;
        var databaseTypeStr = App.Instance.GetConfiguration(true)?["SpeedBoot:DatabaseType"];
        if (!databaseTypeStr.IsNullOrWhiteSpace())
        {
            databaseType = (DatabaseType)int.Parse(databaseTypeStr);
        }

        services.AddSequentialIdGenerator(Utils.GetGuidType(databaseType));
    }
}
