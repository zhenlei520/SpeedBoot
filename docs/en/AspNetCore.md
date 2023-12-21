#AspNetCore

The **SpeedBoot.AspNetCore** class library is a class library that supports **netcore2.1** or above. Through it, each module can be used non-invasively.

## how to use

1. Install **SpeedBoot.AspNetCore** nuget package

2. Add environment variables

    ```json
    "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "SpeedBoot.AspNetCore"
    ```

3. Added service component registration implementation

    ```csharp
    public class ServiceComponent : IServiceComponent
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Registry service logic
        }
    }
    ```

4. Add configuration `appsettings.json`

    ```json
    {
      "SpeedBoot": {
        "AssemblyName": "(BLL|DAL|CRM.*)"//Load the class library whose class library name contains BLL, DAL, CRM.XXX
      }
    }
    ```

    > optional and all assemblies in the application will be loaded by default.