# AspNetCore.Authentication

**SpeedBoot.AspNetCore.Authentication** class library is a class library that supports **netcore2.1** or above, through which user information can be easily obtained

## how to use

1. Install **SpeedBoot.AspNetCore.Authentication** nuget package

2. Add configuration `appsettings.json`

   ```json
   {
       "SpeedBoot": {
           "Identity": {
               "ClaimMappings": {
                   "Id": "sub"//id is the attribute name, sub is the claimType
               }
           }
       }
   }
   ```

   > Optional. By default, only `IUserContext<IdentityUser<Guid>>` and `IUserSetter<IdentityUser<Guid>>` are supported.
   >

## Custom user entity

1. Implement `IIdentityUser<TUserId>`

   ```csharp
   public class CustomUser : IIdentityUser<int>
   {
       public int Id { get; set; }
       
       public string Name { get; set; }
   }
   ```

2. Modify configuration `appsettings.json`

   ```csharp
   {
       "SpeedBoot": {
           "Identity": {
               "ClaimMappings": {
                   "Id": "sub",//id is the attribute name, sub is the claimType
                   "Name": "name"
               },
               "IdentityUserType": "SpeedBoot.AspNetCore.Tests.Infrastructure.CustomUser,SpeedBoot.AspNetCore.Tests",//Complete namespace, assembly name
               "IdentityUserKey": "System.Int32"//UserId type
           }
       }
   }
   ```

   > For details, please view the test project **SpeedBoot.AspNetCore.Tests**

