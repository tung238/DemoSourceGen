using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGenerator.Extensions;
using System.Text.RegularExpressions;

namespace SourceGenerator
{
    public class PropertyModel
    {
        public string Name => syntax.Identifier.ValueText;
        public string NameCamelCase => syntax.Identifier.ValueText.ToCamelCase();
        public string SourceType => syntax.Type.ToString();
        public string DestinationType
        {
            get
            {
                if (!IsCustomType)
                    return syntax.Type.ToString();

                if (syntax.Type is GenericNameSyntax)
                {
                    var str = syntax.Type.ToString();
                    return str.Insert(str.LastIndexOf('>'), "DTO");
                }
                return $"{syntax.Type.ToString()}DTO";
            }
        }

        public string BaseType
        {
            get
            {
                if (syntax.Type is GenericNameSyntax)
                {
                    var str = syntax.Type.ToString();
                    Regex yourRegex = new Regex(@"\<([^\}]+)\>");
                    return yourRegex.Match(str).Groups[1].Value;
                }
                return null;
            }
        }

        public bool IsCustomType { get; set; }

        protected readonly PropertyDeclarationSyntax syntax;

        public PropertyModel(PropertyDeclarationSyntax syntax)
        {
            this.syntax = syntax;
        }
        //public Attribute

    }
}