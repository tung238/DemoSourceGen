using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using SourceGenerator.Extensions;
using SourceGenerator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static SourceGenerator.Constants;

namespace SourceGenerator
{
    [Generator]
    public class MySourceGenerator : ISourceGenerator
    {
        private readonly TemplateGenerator templateGenerator;

        public MySourceGenerator()
        {
            this.templateGenerator = new TemplateGenerator();
        }
        public void Initialize(GeneratorInitializationContext context)
        {
            // Register a factory that can create our custom syntax receiver
            context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
        }

        /// <summary>
        /// Called to perform source generation. 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(GeneratorExecutionContext context)
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif 

            MySyntaxReceiver syntaxReceiver = (MySyntaxReceiver)context.SyntaxReceiver;
            List<string> entities = syntaxReceiver.Entities.Select(e=> e.GetClassName()).ToList();
            foreach (var classDeclarationSyntax in syntaxReceiver.Entities)
            {
                var dtoModel = new DTOModel(classDeclarationSyntax, entities);
                GenerateDTOClasses(context, dtoModel);
                //GenerateMapperExtensions(context, dtoModel);
            }
        }

        private void GenerateDTOClasses(GeneratorExecutionContext context, DTOModel dtoModel)
        {
            try
            {
                var result = this.templateGenerator.Execute(Templates.DTOTemplate, dtoModel);

                //context.TryLogSourceCode(proxy, result);
                //context.ApplyDesignTimeFix(result, proxyModel.ClassName);
                context.AddSource($"{dtoModel.ClassName}", SourceText.From(result, Encoding.UTF8));

            }
            catch (Exception ex)
            {
            }


        }
    }
}