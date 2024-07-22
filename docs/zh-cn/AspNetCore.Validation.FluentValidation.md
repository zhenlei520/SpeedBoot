# AspNetCore.Validation.FluentValidation



**SpeedBoot.AspNetCore.Validation.FluentValidation** 类库是一个支持 **net7.0**以上版本的类库，通过它可以快速完整 **MinimalAPI** 项目的参数验证

## 如何使用

1. 安装 **SpeedBoot.AspNetCore.Validation.FluentValidation** nuget 包

2. 增加用于接受用户参数的对象 **AddUserRequest** 以及对应的验证类 **AddUserRequestValidator**

   ```c#
   public class AddUserRequest
   {
       public string Name { get; set; }
   }
   
   public class AddUserRequestValidator: AbstractValidator<AddUserRequest>
   {
       public AddUserRequestValidator()
       {
           RuleFor(r => r.Name).NotNull().NotEmpty().MaximumLength(20);
       }
   }
   ```

   

3. 在 原 **MinimalAPI** 项目中的方法中增加特性 **AutoValidationAttribute**

   ```c#
   public class ValidationService :ServiceBase
   {
       public ValidationService()
       {
       }
   
       /// <summary>
       /// /api/v1/validations/user
       /// </summary>
       /// <param name="request"></param>
       /// <returns></returns>
       [AutoValidation]
       public IResult AddUser(AddUserRequest request)
       {
           // 到这里的时候，已经通过了参数验证，如果不需要参数验证，可以不写对应的 Validator 即可
           return Results.Ok();
       }
   }
   ```

   > **AutoValidation** 支持方法、类（可参考项目：SpeedBoot.AspNetCore.Tests）