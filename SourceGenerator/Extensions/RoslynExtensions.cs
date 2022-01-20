using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SourceGenerator.Extensions
{
    public static class RoslynExtensions
    {
        public static IEnumerable<INamedTypeSymbol> GetAllWithBaseClass(this Compilation compilation, string baseClass, CancellationToken token)
        {
            var visitor = new TypeVisitor(type => type.TypeKind == TypeKind.Class &&
                                                 type.GetBaseTypes().Any(x => x.ToDisplayString() == baseClass
                                        ), token);
            visitor.Visit(compilation.GlobalNamespace);

            return visitor.GetResults();
        }

        public static IEnumerable<ITypeSymbol> GetBaseTypesAndThis(this ITypeSymbol type)
        {
            var current = type;
            while (current != null)
            {
                yield return current;
                current = current.BaseType;
            }
        }

        public static IEnumerable<ITypeSymbol> GetBaseTypes(this ITypeSymbol type)
        {
            var current = type.BaseType;
            while (current != null)
            {
                yield return current;
                current = current.BaseType;
            }
        }

        public static IEnumerable<ISymbol> GetAllMembers(this ITypeSymbol type)
        {
            return type.GetBaseTypesAndThis().SelectMany(n => n.GetMembers());
        }

        public static CompilationUnitSyntax GetCompilationUnit(this SyntaxNode syntaxNode)
        {
            return syntaxNode.Ancestors().OfType<CompilationUnitSyntax>().FirstOrDefault();
        }

        public static string GetClassName(this ClassDeclarationSyntax syntax) => syntax.Identifier.Text;

        public static string GetClassModifier(this ClassDeclarationSyntax syntax) => syntax.Modifiers.ToFullString().Trim();

        public static List<PropertyModel> GetProperties(this ClassDeclarationSyntax syntax, List<string> entities)
        {
            var properties = syntax.Members.OfType<PropertyDeclarationSyntax>()
                .Select(p => new PropertyModel(p)).ToList();
            foreach(var property in properties)
            {
                if (entities.Any(e => e == property.SourceType || property.SourceType.Contains($"<{e}>")))
                    property.IsCustomType = true;
            }
            return properties;
        }

        public static bool HaveAttribute(this ClassDeclarationSyntax syntax, string attributeName)
        {
            return syntax.AttributeLists.Count > 0 &&
                   syntax.AttributeLists.SelectMany(al => al.Attributes
                                             .Where(a => (a.Name as IdentifierNameSyntax)?.Identifier.Text == attributeName))
                                             .Any();
        }

        public static string GetNamespace(this CompilationUnitSyntax root)
        {
            return root.ChildNodes()
                       .OfType<NamespaceDeclarationSyntax>()
                       .FirstOrDefault()
                       .Name
                       .ToString();
        }

        public static List<string> GetUsings(this CompilationUnitSyntax root)
        {
            return root.ChildNodes()
                       .OfType<UsingDirectiveSyntax>()
                       .Select(n => n.Name.ToString())
                       .ToList();
        }
    }
}
