# AspNetCore

**SpeedBoot.AspNetCore**类库是一个支持**netcore2.1**以上版本的类库，通过它可以无侵入性的使用各模块

## 如何使用

1. 安装 **SpeedBoot.AspNetCore** nuget 包

2. 添加环境变量

   ```json
   "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "SpeedBoot.AspNetCore"
   ```

3. 新增服务组件注册实现

   ```csharp
   public class ServiceComponent : IServiceComponent
   {
       public void ConfigureServices(IServiceCollection services)
       {
           //Registry service logic
       }
   }
   ```

4. 增加配置`appsettings.json`

   ```json
   {
     "SpeedBoot": {
       "AssemblyName": "(BLL|DAL|CRM.*)"//加载类库名包含BLL、DAL、CRM.XXX的类库
     }
   }
   ```

   > 非必填项，默认会加载应用程序中所有的程序集