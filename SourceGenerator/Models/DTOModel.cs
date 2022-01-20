using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SourceGenerator.Extensions;

namespace SourceGenerator.Models
{
    internal class DTOModel : ClassModel
    {
        public DTOModel(ClassDeclarationSyntax classSyntax, List<string> entities) : base(classSyntax, entities)
        {
            ClassName = $"{classSyntax.GetClassName()}DTO";
            Usings.Add("System.Linq");
            Usings.Add(Namespace);
            Namespace = Namespace.ReplaceLastPathWith("DTOs");
        }
    }
}