using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGenerator.Models
{
    internal class ClassModel
    {
        protected readonly List<string> entities;
        protected readonly ClassDeclarationSyntax classSyntax;
        //protected readonly CompilationUnitSyntax root;
        //protected readonly SemanticModel classSemanticModel;
        //protected readonly INamedTypeSymbol classSymbol;
        public List<PropertyModel> Properties => classSyntax.GetProperties(entities);
        public string ClassName { get; set; }

        public string Namespace { get; set; }

        public string ClassBase => classSyntax.GetClassName();

        public string ClassModifier => classSyntax.GetClassModifier();

        public List<string> Usings { get; set; }

        public ClassModel(ClassDeclarationSyntax classSyntax, List<string> entities)
        {
            this.entities = entities;
            this.classSyntax = classSyntax;
            var root = classSyntax.GetCompilationUnit();

            Usings = root.GetUsings();
            Namespace = root.GetNamespace();
            ClassName = classSyntax.GetClassName();
        }
    }
}
