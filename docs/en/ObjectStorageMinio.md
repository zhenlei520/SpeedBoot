# MinioStorage

Provides basic object storage functions, which can greatly reduce the workload of connecting Minio storage

## use

There are two situations when using Minio to store `SpeedBoot.ObjectStorage.Minio`:

1. Use of AspNetCore project (Web project)
2. Used by non-AspNetCore projects (non-Web projects)

### Scenario 1: AspNetCore project

Minio storage implements automatic module loading, so the **Web** project can be configured as agreed after installing `SpeedBoot.AspNetCore`. The specific operations are as follows:

1. Install `SpeedBoot.AspNetCore`

    ```shell
    dotnet add package SpeedBoot.AspNetCore
    ```

2. Configure environment variables

    Taking the development environment as an example, modify **Properties/launchSettings.json**. For online environment, please refer to [Modify environment variables](https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/environments? view=aspnetcore-7.0#set-environment-on-the-command-line)

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

    > The value of the environment variable ASPNETCORE_HOSTINGSTARTUPASSEMBLIES is a fixed value: SpeedBoot.AspNetCore

    > Steps 1 and 2 are shared by the **AspNetCore** project and are not exclusive features of **Minio Storage**

3. Install `SpeedBoot.ObjectStorage.Minio`

    ```shell
    dotnet add package SpeedBoot.ObjectStorage.Minio
    ```

4. Add `minio` storage configuration

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

5. Obtain `IObjectStorageClient` or `IObjectStorageClientContainer` through dependency injection in the project and use it

### Scenario 2: Non-AspNetCore project

1. Install `SpeedBoot.ObjectStorage.Minio`

    ```shell
    SpeedBoot.ObjectStorage.Minio
    ```

2. Add `Minio` storage configuration

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

3. Register to the service collection

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

    > Make sure the configuration file is copied to the project running directory

4. Obtain `IObjectStorageClient` or `IObjectStorageClientContainer` through dependency injection in the project and use it