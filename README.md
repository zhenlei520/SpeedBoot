[中](README.zh-CN.md) | EN

# SpeedBoot

`SpeedBoot` is a rapidly developed core program designed to help us improve development efficiency. It provides a large number of class libraries based on the **netstandard2.0** framework. Their applicability is wider. It also provides **netcore** **Web** project makes developing a web project easier, making your code more concise and refreshing through non-intrusive coding

## Directory Structure

````
├── docs 文档
├── src                                                        
│   ├── Data								                   
│   │   ├── Speed.Boot.Data.Abstractions                         Data Abstraction Class Library
│   │   ├── Speed.Boot.Data.DependencyInjection.Abstractions     Data abstraction class library that supports DI
│   │   ├── Speed.Boot.Data.EFCore                               is implemented based on EFCore
│   │   ├── Speed.Boot.Data.EFCore.Pomelo.MySql                  MySql database implementation based on EFCore
│   │   ├── Speed.Boot.Data.EFCore.PostgreSql                    database implementation based on EFCore
│   │   ├── Speed.Boot.Data.EFCore.Sqlite                        Sqlite database implementation based on EFCore
│   │   ├── Speed.Boot.Data.EFCore.SqlServer                     SqlServer database implementation based on EFCore
│   │   ├── Speed.Boot.Data.FreeSql                              is implemented based on FreeSql
│   │   ├── Speed.Boot.Data.FreeSql.MySql                        MySql database implementation based on FreeSql
│   │   ├── Speed.Boot.Data.FreeSql.PostgreSql                   PostgreSql database implementation based on FreeSql
│   │   ├── Speed.Boot.Data.FreeSql.Sqlite                       database implementation based on FreeSql
│   │   └── Speed.Boot.Data.FreeSql.SqlServer                    SqlServer database implementation based on FreeSql
│   ├── Extensions                            				   
│   │   ├── Configuration                                      
│   │   │   ├── SpeedBoot.Extensions.Configuration               Extension class library based on configuration
│   │   │   └── SpeedBoot.Extensions.Configuration.Json          Extension class library based on Json configuration file
│   │   ├── DependencyInjection                                
│   │   │   ├── SpeedBoot.Extensions.DependencyInjection.Abstractions  DI-based abstraction
│   │   │   └── SpeedBoot.Extensions.DependencyInjection         DI implementation based on Microsoft.Extensions.DependencyInjection
│   ├── IdGenerator                            				   
│   │   │   ├── SpeedBoot.Extensions.IdGenerator        
│   │   │   ├── SpeedBoot.Extensions.IdGenerator.Normal          Default Guid Generator
│   │   │   └── SpeedBoot.Extensions.IdGenerator.Sequential      Ordered Guid Generator
│   ├── ObjectStorage                                          
│   │   │   ├── SpeedBoot.ObjectStorage.Abstractions            object storage abstract class library
│   │   │   ├── SpeedBoot.ObjectStorage.Aliyun                  based on Aliyun Oss
│   │   │   └── SpeedBoot.ObjectStorage.Minio                   Implementation based on Minio Oss
│   ├── Security                                               
│   │   │   └── SpeedBoot.Security.Cryptography                 password-related help library
│   ├── SpeedBoot.AspNetCore                                    implementation of SpeetBoot helps to use various implementations without any sense.
│   ├── SpeedBoot.Core                                          provides an implementation based on NetCore and provides module registration function
│   ├── SpeedBoot.SourceGenerator                               will later provide support for generating multi-language exceptions
│   ├── SpeedBoot.System                                        common help libraries as well as multi-language, framework exceptions, etc.
│   ├── SpeedBoot.System.Net                                   provides network-related help classes
├── test                                                       unit test of each module
├── .gitignore                                                 ignored file submitted by git
├── LICENSE.md                                                 project license
├── Directory.Build.props                                      project compilation file
├── README.md                                                  English help document
└── README.zh-CN.md                                            Chinese help document

````

## Naming rules

Named: SpeetBoot.XXX

The projects with **.Core** in the project name are **netcore** projects, those with **AspNetCore** refer to the **Web** project of **netcore**, and other projects are supported **netstandard2.0** and higher versions of the **net** framework

## More documentation

[View module documentation](./docs/en/README.md)