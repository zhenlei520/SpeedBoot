# AspNetCore.Authentication

**SpeedBoot.AspNetCore.Authentication** 类库是一个支持 **netcore2.1** 以上版本的类库，通过它可以方便获取用户信息

## 如何使用

1. 安装 **SpeedBoot.AspNetCore.Authentication** nuget 包

2. 增加配置`appsettings.json`

   ```json
   {
       "SpeedBoot": {
           "Identity": {
               "ClaimMappings": {
                   "Id": "sub"//id为属性名、sub为claimType
               }
           }
       }
   }
   ```
   
   > 非必填项，默认仅支持 `IUserContext<IdentityUser<Guid>>`、`IUserSetter<IdentityUser<Guid>>`