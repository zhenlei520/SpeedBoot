中 | [EN](README.md)

# SpeedBoot

`SpeedBoot`是一个快速开发的核心程序，旨在帮助我们提升开发效率，其中提供了大量基于 **netstandard2.0** 框架的类库，它们的适用性更广泛，也提供了基于 **netcore**的**Web**项目，使得开发一个web项目更加简单，通过无侵入性的编码让你的代码更简洁清爽

## 目录结构

````
├── docs 文档
├── src                                                        源文件目录
│   ├── Data								                   数据相关
│   │   ├── SpeedBoot.Data.Abstractions                       数据抽象类库
│   │   ├── SpeedBoot.Data.DependencyInjection.Abstractions   支持DI的数据抽象类库
│   │   ├── SpeedBoot.Data.EFCore                             基于EFCore实现
│   │   ├── SpeedBoot.Data.EFCore.Pomelo.MySql                基于EFCore的MySql数据库实现
│   │   ├── SpeedBoot.Data.EFCore.PostgreSql                  基于EFCore的PostgreSql数据库实现
│   │   ├── SpeedBoot.Data.EFCore.Sqlite                      基于EFCore的Sqlite数据库实现
│   │   ├── SpeedBoot.Data.EFCore.SqlServer                   基于EFCore的SqlServer数据库实现
│   │   ├── SpeedBoot.Data.FreeSql                            基于FreeSql实现
│   │   ├── SpeedBoot.Data.FreeSql.MySql                      基于FreeSql的MySql数据库实现
│   │   ├── SpeedBoot.Data.FreeSql.PostgreSql                 基于FreeSql的PostgreSql数据库实现
│   │   ├── SpeedBoot.Data.FreeSql.Sqlite                     基于FreeSql的Sqlite数据库实现
│   │   └── SpeedBoot.Data.FreeSql.SqlServer                  基于FreeSql的SqlServer数据库实现
│   ├── Extensions                            				   扩展类库
│   │   ├── Configuration                                      配置
│   │   │   ├── SpeedBoot.Extensions.Configuration             基于 配置的扩展类库
│   │   │   └── SpeedBoot.Extensions.Configuration.Json        基于 Json配置文件的扩展类库
│   │   ├── DependencyInjection                                依赖注入
│   │   │   ├── SpeedBoot.Extensions.DependencyInjection.Abstractions        基于DI的抽象
│   │   │   └── SpeedBoot.Extensions.DependencyInjection       基于 Microsoft.Extensions.DependencyInjection 的DI实现
│   ├── IdGenerator                            				   Id生成器
│   │   │   ├── SpeedBoot.Extensions.IdGenerator
│   │   │   ├── SpeedBoot.Extensions.IdGenerator.Normal        默认Guid生成器
│   │   │   └── SpeedBoot.Extensions.IdGenerator.Sequential    有序的Guid生成器
│   ├── ObjectStorage                                          对象存储
│   │   │   ├── SpeedBoot.ObjectStorage.Abstractions           对象存储抽象类库
│   │   │   ├── SpeedBoot.ObjectStorage.Aliyun                 基于 Aliyun Oss 的实现
│   │   │   └── SpeedBoot.ObjectStorage.Minio                  基于 Minio Oss的实现
│   ├── Security                                               安全
│   │   │   └── SpeedBoot.Security.Cryptography                密码相关的帮助类库
│   ├── SpeedBoot.AspNetCore                                   SpeetBoot的AspNetCore实现，有助于无感使用各种实现
│   ├── SpeedBoot.Core                                         提供了基于NetCore的实现，提供模块注册功能
│   ├── SpeedBoot.SourceGenerator                              后续为生成多语言异常提供支持
│   ├── SpeedBoot.System                                       提供了常用帮助类库以及多语言、框架异常等
│   ├── SpeedBoot.System.Net                                   提供了与网络有关的帮助类
├── test                                                       各模块的单元测试
├── .gitignore                               git提交的忽略文件
├── LICENSE.md                               项目许可
├── Directory.Build.props                    项目编译文件
├── README.md                                英文帮助文档
└── README.zh-CN.md                          中文帮助文档
````

## 命名规则

命名为：SpeetBoot.XXX

项目名中存在**.Core**的为**netcore**项目、以**AspNetCore**的指的是**netcore**的**Web**项目，除此之外的项目则支持**netstandard2.0**及更高版本的 **net**框架

## 更多文档

[查看模块文档](./docs/zh-cn/README.md)
