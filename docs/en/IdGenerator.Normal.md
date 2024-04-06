# Unordered Guid generator

Provides the function of an unordered Guid generator, which facilitates the subsequent replacement of other Id generators.

## How to use

1. Install **SpeedBoot.Extensions.IdGenerator.Normal**

2. You can get the IIdGenerator through **DI**, or you can get the **IIdGenerator** object through ``` App.Instance.GetIdGenerator() or App.Instance.GetRequiredIdGenerator() ```, or you can use ``` App.Instance.GeneratorGuid()``` Get the value of Guid

   > The above method is only available when using one Guid generator