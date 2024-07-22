# AspNetCore.Validation.FluentValidation



**SpeedBoot.AspNetCore.Validation.FluentValidation** class library is a class library that supports **net7.0** and above versions, through which you can quickly and completely verify the parameters of the **MinimalAPI** project

## How to use

1. Install **SpeedBoot.AspNetCore.Validation.FluentValidation** nuget package

2. Add an object for accepting user parameters **AddUserRequest** and the corresponding verification class **AddUserRequestValidator**

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

   

3. Add the attribute **AutoValidationAttribute** to the method in the original **MinimalAPI** project

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
           // When you get here, you have passed parameter verification. If you don’t need parameter verification, you don’t need to write the corresponding Validator.
           return Results.Ok();
       }
   }
   ```

   > **AutoValidation** supports methods and classes (refer to the project: SpeedBoot.AspNetCore.Tests)