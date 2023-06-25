﻿中 | [EN](README.md)

# SpeedBoot

`SpeedBoot`是一个快速开发的核心程序，通过它可以帮助我们提升开发效率

## 如何使用

### AspNetCore 应用程序

1. 添加环境变量

   ```json
   "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "SpeedBoot.AspNetCore"
   ```

2. 新增服务组件注册实现

   ```csharp
   public class ServiceComponent : IServiceComponent
   {
       public void ConfigureServices(IServiceCollection services)
       {
           //Registry service logic
       }
   }
   ```

   

3. 增加配置`appsettings.json`

   ```json
   {
     "SpeedBoot": {
       "AssemblyName": "(BLL|DAL|CRM.*)"//加载类库名包含BLL、DAL、CRM……的类库
     }
   }
   ```

   > 步骤2是非必填项，默认会加载应用程序中所有的程序集
