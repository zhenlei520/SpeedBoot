# 有序 Guid 生成器

提供了有序Guid生成器的功能

## 如何使用

1. 安装 **SpeedBoot.Extensions.IdGenerator.Sequential**
2. 通过 **DI**获取到 IIdGenerator 即可 或者通过 ``` App.Instance.GetIdGenerator() 或者 App.Instance.GetRequiredIdGenerator() ``` 即可得到 **IIdGenerator** 对象，也可通过 ```App.Instance.GeneratorGuid()``` 得到 Guid的值

## 疑问

1. 不同数据库的 Id 都一样吗？

   不同数据库使用Guid生成器不同，通过修改 `appsettings.json` 配置文件可支持修改支持不同的数据库

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

   > 数据库类型：
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