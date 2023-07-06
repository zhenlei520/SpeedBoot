# Minio存储

提供了基础的对象存储功能，通过它可以极大减轻对接Minio存储的工作量

## 使用

使用Minio存储`SpeedBoot.ObjectStorage.Minio`分两种情况：

1. AspNetCore 项目（Web项目）使用
2. 非AspNetCore项目（非Web项目）使用

### 场景1：AspNetCore 项目

Minio存储实现了模块自动加载，因此 **Web** 项目安装`SpeedBoot.AspNetCore`后按照约定配置即可，具体操作如下：

1. 安装 `SpeedBoot.AspNetCore`

   ```shell
   dotnet add package SpeedBoot.AspNetCore
   ```

2. 配置环境变量

   以开发环境为例，修改 **Properties/launchSettings.json**，线上环境请参考 [修改环境变量](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/environments?view=aspnetcore-7.0#set-environment-on-the-command-line)

   ```json
   {
       "$schema":"https://json.schemastore.org/launchsettings.json",
       "profiles":{
           "WebApplication.Service":{
               "commandName":"Project",
               "dotnetRunMessages":true,
               "launchBrowser":true,
               "launchUrl":"swagger",
               "applicationUrl":"https://localhost:7124;http://localhost:5102",
               "environmentVariables":{
                   "ASPNETCORE_ENVIRONMENT":"Development",
                   "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES":"SpeedBoot.AspNetCore"
               }
           }
       }
   }
   ```

   > 环境变量 ASPNETCORE_HOSTINGSTARTUPASSEMBLIES 的值为固定值：SpeedBoot.AspNetCore

   > 步骤1、2 属于 **AspNetCore** 项目共有，并非 **Minio存储** 独享的功能

3. 安装 `SpeedBoot.ObjectStorage.Minio`

   ```shell
   dotnet add package SpeedBoot.ObjectStorage.Minio
   ```

4. 增加 `minio` 存储配置

   ```json
   {
     "MinioObjectStorage": {
       "AccessKeyId": "AccessKey",
       "AccessKeySecret": "AccessKeySecret",
       "Endpoint": "localhost:9000",
       "BucketName": "temp",
       "EnableHttps": false
     }
   }
   ```

5. 在项目中通过依赖注入获取 `IObjectStorageClient` 或 `IObjectStorageClientContainer` 使用即可

### 场景2：非 AspNetCore 项目

1. 安装 `SpeedBoot.ObjectStorage.Minio`

   ```shell
   SpeedBoot.ObjectStorage.Minio
   ```

2. 增加 `Minio` 存储配置

   ```json
   {
     "MinioObjectStorage": {
       "AccessKeyId": "AccessKey",
       "AccessKeySecret": "AccessKeySecret",
       "Endpoint": "localhost:9000",
       "BucketName": "temp",
       "EnableHttps": false
     }
   }
   ```

3. 注册到服务集合

   ```csharp
   var services = new ServiceCollection();
   var file = "appsettings.json";
   var configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile(file)
       .Build();
   AppCore.ConfigureConfiguration(configuration);
   services.AddMinio();
   ```

   > 确保配置文件被复制到项目运行目录

4. 在项目中通过依赖注入获取 `IObjectStorageClient` 或 `IObjectStorageClientContainer` 使用即可
