# Aliyun Storage OSS

Provides basic object storage functions, which can greatly reduce the workload of connecting to Aliyun storage

## use

There are two situations when using Aliyun Storage `SpeedBoot.ObjectStorage.Aliyun`:

1. Use of AspNetCore project (Web project)
2. Used by non-AspNetCore projects (non-Web projects)

### Scenario 1: AspNetCore project

Aliyun Storage implements automatic module loading, so the **Web** project can be configured as agreed after installing `SpeedBoot.AspNetCore`. The specific operations are as follows:

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

    > Steps 1 and 2 are common to the **AspNetCore** project and are not exclusive functions of **Aliyun Storage**

3. Install `SpeedBoot.ObjectStorage.Aliyun`

    ```shell
    dotnet add package SpeedBoot.ObjectStorage.Aliyun
    ```

4. Add Aliyun storage configuration

    ```json
    {
      "AliyunObjectStorage": {
        "AccessKeyId": "AccessKeyId-storage",
        "AccessKeySecret": "AccessKeySecret-storage",
        "Endpoint": "Endpoint-storage",
        "CallbackUrl": "CallbackUrl-storage",
        "CallbackBody": "CallbackBody-storage",
        "EnableResumableUpload": true,
        "BigObjectContentLength": 100,
        "PartSize": 1024,
        "Quiet": true,
        "BucketName": "BucketName-storage"
      }
    }
    ```

5. Obtain `IObjectStorageClient` or `IObjectStorageClientContainer` through dependency injection in the project and use it

### Scenario 2: Non-AspNetCore project

1. Install `SpeedBoot.ObjectStorage.Aliyun`

    ```shell
    SpeedBoot.ObjectStorage.Aliyun
    ```

2. Add Aliyun storage configuration

    ```json
    {
      "AliyunObjectStorage": {
        "AccessKeyId": "AccessKeyId-storage",
        "AccessKeySecret": "AccessKeySecret-storage",
        "Endpoint": "Endpoint-storage",
        "CallbackUrl": "CallbackUrl-storage",
        "CallbackBody": "CallbackBody-storage",
        "EnableResumableUpload": true,
        "BigObjectContentLength": 100,
        "PartSize": 1024,
        "Quiet": true,
        "BucketName": "BucketName-storage"
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
    services.AddAliyunStorage();
    ```

    > Make sure the configuration file is copied to the project running directory

4. Obtain `IObjectStorageClient` or `IObjectStorageClientContainer` through dependency injection in the project and use it

## Others

### Aliyun Storage Configuration

Aliyun storage configuration supports three formats:

1. Use STS (temporary credentials) through RAM role

    ```json
    {
      "AliyunObjectStorage": {
        "AccessKeyId": "AccessKeyId of RAM user (need to include permission to call sts)",
        "AccessKeySecret": "RAM user (needs to include permission to call sts) AccessKeySecret",
        "Endpoint": "oss-cn-shanghai.aliyuncs.com (domain name address)",
        "BucketName": "storage1-test",
        "Sts": {
          "RegionId": "shanghai (region)",
          "RoleArn": "acs:ram::1658466362075755:role/storage-temp (role Arn)",
          "RoleSessionName": "storage (session name of temporary token)"
        }
      }
    }
    ```

    > Endpoint: https://help.aliyun.com/document_detail/31837.html
    >
    > BucketName: Use the BucketName of `IObjectStorageClientContainer`

    1. Create RAM user![create-ram-user](../../../assets/storage/aliyun/create-ram-user.png)

    2. Set RAM user permissions (including STS)![set-sts-permission](../../../assets/storage/aliyun/ram-user-set-sts-permission.jpg)

    3. Create RAM role

       ![create-role](../../../assets/storage/aliyun/create-ram-role-1.jpg) ![create-role](../../../assets/ storage/aliyun/create-ram-role-2.jpg)

    4. Set RAM role permissions

       ![set-role-permission](../../../assets/storage/aliyun/ram-role-set-permission.jpg)

2. Via RAM user key and password

    ```json
    {
      "AliyunObjectStorage": {
        "AccessKeyId": "AccessKeyId of RAM user (must include Oss permission)",
        "AccessKeySecret": "RAM user (requires Oss permission) AccessKeySecret",
        "Endpoint": "oss-cn-shanghai.aliyuncs.com (domain name address)",
        "BucketName": "storage1-test"
      }
    }
    ```

    1. Create RAM user![create-ram-user](../../../assets/storage/aliyun/create-ram-user.png)

    2. Set RAM user permissions (including OSS)![set-sts-permission](../../../assets/storage/aliyun/ram-user-set-oss-permission.jpg)

    3. Use the main account account key and password

       ```json
       {
         "AliyunObjectStorage": {
           "AccessKeyId": "AccessKeyId of the main account",
           "AccessKeySecret": "AccessKeySecret of the main account",
           "Endpoint": "oss-cn-shanghai.aliyuncs.com (domain name address)",
           "BucketName": "storage1-test"
         }
       }
       ```

       Use the `AccessKeyId` and `AccessKeySecret` of the main account![aliyun-secret](../../../assets/storage/aliyun/aliyun-secret.jpg)
       > If you are using the key and password of the main account, you can also use the following configuration

       ```json
       {
           "Aliyun":{
               "AccessKeyId":"AccessKeyId of the main account",
               "AccessKeySecret":"AccessKeySecret of the master account"
           },
           "AliyunObjectStorage":{
               "Endpoint":"oss-cn-shanghai.aliyuncs.com (domain name address)",
               "BucketName":"storage1-test"
           }
       }
       ```

       > The main account key can be applied to all Aliyun products. If the storage account uses the main account, using the above configuration can reduce repeated key configurations (but for security reasons, it is not recommended to use the main account configuration)