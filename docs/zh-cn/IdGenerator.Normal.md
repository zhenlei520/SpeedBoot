# 无序 Guid 生成器

提供了无序Guid生成器的功能，为后续更换其他 Id 生成器提供了便利

## 如何使用

1. 安装 **SpeedBoot.Extensions.IdGenerator.Normal**

2. 通过 **DI**获取到 IIdGenerator 即可 或者通过 ``` App.Instance.GetIdGenerator() 或者 App.Instance.GetRequiredIdGenerator() ``` 即可得到 **IIdGenerator** 对象，也可通过 ```App.Instance.GeneratorGuid()``` 得到 Guid的值

   > 以上办法仅使用一种 Guid 生成器时可用