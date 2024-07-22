# Ordered Guid generator

Provides functionality for ordered Guid generators

## How to use

1. Install **SpeedBoot.Extensions.IdGenerator.Sequential**
2. You can get the IIdGenerator through **DI**, or you can get the **IIdGenerator** object through ``` App.Instance.GetIdGenerator() or App.Instance.GetRequiredIdGenerator() ```, or you can use ``` App.Instance.GeneratorGuid()``` Get the value of Guid

   > The above method is only available when using one Guid generator

## Questions

1. Are the IDs in different databases the same?

   Different databases use different Guid generators. By modifying the `appsettings.json` configuration file, you can support modifications to support different databases.

   ```json
   {
     "SpeedBoot": {
         "IdGenerator":{
             "DatabaseType": 0
         }
     },
     "AllowedHosts": "*"
   }

   ```

   > Database type：
   >
   > ```c#
   > public enum DatabaseType
   > {
   >     SqlServer = 0,
   >     MySql = 1,
   >     PostgreSql = 2,
   >     Oracle = 3
   > }
   > ```