using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SourceGenerator
{
    public class MySyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> Entities { get; private set; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
            {
                var tds = (TypeDeclarationSyntax)syntaxNode;

                if (tds.BaseList != null)
                {
                    var baselist = tds.BaseList;

                    foreach (var entry in baselist.Types)
                    {
                        if (entry is SimpleBaseTypeSyntax basetype)
                        {
                            if (basetype.Type.ToString() == "BaseEntity")
                            {
                                Entities.Add(classDeclarationSyntax);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}