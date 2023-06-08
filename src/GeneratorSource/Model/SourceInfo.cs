// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System.Text;

namespace GeneratorSource.Utils;

internal class SourceInfo
{
    public List<string> UseNamespaces { get; set; } = new();

    public string Namespace { get; set; }

    public string ClassName { get; set; }

    public bool IsStatic { get; set; }

    public bool IsInternal { get; set; } = true;

    public string Content { get; set; }

    public List<string> SupportNamespaces { get; set; } = new();

    /// <param name="ns">class namespace</param>
    /// <param name="className"></param>
    /// <param name="isStatic">is static</param>
    /// <param name="isInternal"></param>
    public SourceInfo(string ns, string className, bool isStatic, bool isInternal = true)
    {
        Namespace = ns;
        ClassName = className;
        IsStatic = isStatic;
        IsInternal = isInternal;
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("// Copyright (c) zhenlei520 All rights reserved.");
        stringBuilder.AppendLine("// Licensed under the MIT License. See LICENSE.txt in the project root for license information.");
        UseNamespaces.ForEach(ns =>
        {
            stringBuilder.AppendLine($"using {ns};");
        });
        stringBuilder.AppendLine("");
        SupportNamespaces.ForEach(ns =>
        {
            stringBuilder.AppendLine($"[assembly: InternalsVisibleTo(\"{ns}\")]");
        });
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine("// ReSharper disable once CheckNamespace");
        stringBuilder.AppendLine($"namespace {Namespace};");
        stringBuilder.AppendLine("");
        stringBuilder.AppendLine(IsInternal ?
            $"internal{(IsStatic ? " static" : "")} class {ClassName}" :
            $"public{(IsStatic ? " static" : "")} class {ClassName}");
        stringBuilder.AppendLine("{");
        stringBuilder.AppendLine(Content);
        stringBuilder.AppendLine("}");
        return stringBuilder.ToString();
    }
}
