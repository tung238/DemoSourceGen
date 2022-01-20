using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGenerator.Extensions
{
    internal static class StringExtensions
    {
        public static string ReplaceLastPathWith(this string @namespace, string path)
        {
            var returnValue = @namespace;
            var index = returnValue.LastIndexOf('.');
            if (index != -1)
            {
                returnValue = returnValue.Remove(index);
            }
            return $"{returnValue}.{path}";
        }

        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1) return char.ToLowerInvariant(str[0]) + str.Substring(1);
            return str;
        }
    }
}
