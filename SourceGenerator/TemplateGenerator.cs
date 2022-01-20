using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SourceGenerator
{
    public class TemplateGenerator
    {
        Dictionary<string,Template> templates = new Dictionary<string, Template>();
        public string Execute(string templateName, object model)
        {
            Template template = null;
            if(!templates.TryGetValue(templateName, out template))
            {
                var templateContent = ResourceReader.GetResource(templateName);
                template = Template.Parse(templateContent);
                if (template.HasErrors)
                {
                    var errors = string.Join(" | ", template.Messages.Select(x => x.Message));
                    throw new InvalidOperationException($"Template parse error: {template.Messages}");
                }
                templates.Add(templateName, template);
            }

            var result = template.Render(model, member => member.Name);

            result = SyntaxFactory.ParseCompilationUnit(result)
                                  .NormalizeWhitespace()
                                  .GetText()
                                  .ToString();

            return result;
        }
    }
}
