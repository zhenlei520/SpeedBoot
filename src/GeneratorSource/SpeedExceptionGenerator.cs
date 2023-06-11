// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

// using System.Diagnostics;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using GeneratorSource.Internal.Model;
using Microsoft.CodeAnalysis;

[assembly: InternalsVisibleTo("Speed.System.Tests")]

namespace GeneratorSource;

[Generator]
internal class SpeedExceptionGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // Debugger.Launch();
    }

    public void Execute(GeneratorExecutionContext context)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("public static readonly Dictionary<long, string> Data = new Dictionary<long, string>()");
        stringBuilder.AppendLine("{");
        foreach (var item in GetExceptionMessage())
        {
            stringBuilder.AppendLine("{" + item.Key + ",\"" + item.Value + "\"},");
        }
        stringBuilder.AppendLine("};");

        var sourceInfo = new SourceInfo("System", "SpeedExceptionResource", false)
        {
            UseNamespaces = new List<string>
            {
                "System.Runtime.CompilerServices"
            },
            SupportNamespaces = new List<string>
            {
                "Speed.System"
            },
            Content = stringBuilder.ToString()
        };
        context.AddSource("SpeedExceptionResource", sourceInfo.ToString());
    }

    public static Dictionary<long, string> GetExceptionMessage()
    {
        var classType = typeof(ExceptionCode);
        var fields = classType.GetFields(BindingFlags.Static | BindingFlags.Public);
        var data = new Dictionary<long, string>();
        foreach (var field in fields)
        {
            var key = long.Parse(field.GetRawConstantValue().ToString());
            data.Add(key, field.GetDescription());
        }
        return data;
    }
}