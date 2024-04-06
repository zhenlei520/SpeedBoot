## Extensions.DependencyInjection

Automatic injection according to agreement

## How to use

1.  Install **SpeedBoot.Extensions.DependencyInjection** nuget package

2. Implement `ISingletonDependency`, `IScopedDependency`, `ITransientDependency` as needed

   > ISingletonDependency: Singleton
   >
   > IScopedDependency: Scoped
   >
   > ITransientDependency：Transient

## Questions

1. Is the interface implementing `ISingletonDependency`, `IScopedDependency`, `ITransientDependency` or class inheritance?

   Regardless of interface or class, the interface or class that inherits `ISingletonDependency`, `IScopedDependency`, `ITransientDependency` is the object to be injected. If it is an interface, the corresponding implementation will be automatically found and registered in the service collection. If it is a class, then The current class will be injected into the service collection, and the implementation can be consistent with what is obtained from DI when used. For example:

   * When implemented as a class:

     ```c#
     public class UserBLL: ISingletonDependency
     {
         public string GetUserName()
         {
             return "zhenlei520";
         }
     }

     /// <summary>
     /// api/v1/users/username
     /// </summary>
     /// <param name="userBll"></param>
     /// <returns></returns>
     public string GetUserName(UserBLL userBll)
     {
         return userBll.GetUserName();
     }
     ```

   * When implemented as an interface:

     ```c#
     public interface IUserDAL : ISingletonDependency
     {
         string GetUserName();
     }

     public class UserDAL : IUserDAL
     {
         public string GetUserName()
         {
             return "zhenlei520";
         }
     }

     /// <summary>
     /// api/v1/users/username2
     /// </summary>
     /// <param name="userDal"></param>
     /// <returns></returns>
     public string GetUserName2(IUserDAL userDal)
     {
         return userDal.GetUserName();
     }
     ```

