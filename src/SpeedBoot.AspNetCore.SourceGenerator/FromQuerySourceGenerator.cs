// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore;

[Generator]
public class FromQuerySourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {

    }

    public void Execute(GeneratorExecutionContext context)
    {
        var sourceCode = @"
            using System;

            namespace GeneratedCode
            {
                public class HelloWorld
                {
                    public static void SayHello()
                    {
                        Console.WriteLine(""Hello from generated code!"");
                    }
                }
            }
        ";

        context.AddSource("HelloWorldGenerator", SourceText.From(sourceCode, Encoding.UTF8));
    }
}
