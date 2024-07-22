## Extensions.DependencyInjection

按约定自动注入

## 如何使用

1.  安装 **SpeedBoot.Extensions.DependencyInjection** nuget 包

2. 根据需要实现 `ISingletonDependency`、`IScopedDependency`、`ITransientDependency`

   > ISingletonDependency: 单例
   >
   > IScopedDependency: 请求
   >
   > ITransientDependency：瞬时

## 疑问

1. 是 接口实现 `ISingletonDependency`、`IScopedDependency`、`ITransientDependency` 还是类继承？

   不区分接口还是类，继承 `ISingletonDependency`、`IScopedDependency`、`ITransientDependency`的接口或者类是待注入的对象，如果是接口，则会自动找到对应的实现注册到服务集合中，如果是类，则会将当前类注入到服务集合，实现的与使用时从DI获取的一致即可。例如：

   * 当实现为类时：

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

   * 当实现为接口时：

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

     