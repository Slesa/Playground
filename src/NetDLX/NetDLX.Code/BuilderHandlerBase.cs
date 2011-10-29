using System;
using System.Text.RegularExpressions;

namespace NetDLX.Code
{
    public class BuilderHandlerBase
    {
        public static Regex ReplaceSpacesRegex = new Regex(@"\s+");

        public static string[] GetLinesFromSource(string sourceCode)
        {
            if (sourceCode == null) throw new ArgumentNullException("sourceCode");
            var lines = Regex.Split(NormalizeSpaces(sourceCode), Environment.NewLine);
            return lines;
        }

        public static string NormalizeSpaces(string source)
        {
            return ReplaceSpacesRegex.Replace(source.Trim(), @" ");
        }

        public static bool IsLabel(string line)
        {
            return line.EndsWith(":");
        }

        public static bool IsComment(string line)
        {
            return line.StartsWith(";");
        }

        public static string[] SeparateLine(string line)
        {
            return line.Split(' ');
        }
        
    }
}