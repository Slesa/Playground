using System;
using System.Text.RegularExpressions;

namespace NetDLX.Code
{
    public class ProgramBuilder
    {
        Program program;
        Regex regex = new Regex(@"\s+");

        public Program Generate(string sourceCode)
        {
            var lines = Regex.Split(regex.Replace(sourceCode.Trim(), @" "), Environment.NewLine);
            program = new Program();

            foreach (var line in lines)
            {
                if (line.StartsWith(";")) continue;
                var restOfLine = line;
                restOfLine = HandleLabel(restOfLine);
            }
            
            return program;
        }

        string HandleLabel(string line)
        {
            var labelName = ExtractLabelName(out line, line);
            if (labelName == null) return line;

            var labelType = ExtractLabelType(out line, line);
            Label label;
            if (labelType == null)
            {
                label = new Label {Name = labelName, Type = LabelType.JUMP};
                program.AddLabel(label);
                return line;
            }

            LabelType type;
            if (!LabelType.TryParse(labelType, out type))
                return line;
            var labelValue = ExtractLabelValue(line);
            label = new Label {Name = labelName, Type = type, Value = labelValue};
            program.AddLabel(label);
            return line;
        }

        string ExtractLabelValue(string line)
        {
            if (String.IsNullOrEmpty(line)) return null;
            return SeparateLine(line)[0];
        }

        string ExtractLabelType(out string newLine, string line)
        {
            newLine = line;
            if (String.IsNullOrEmpty(line)) return null;
            var separation = SeparateLine(line);
            var dataType = separation[0].ToUpper();
            if( dataType==null || !dataType.StartsWith("."))
                return null;

            newLine = line.Substring(dataType.Length+1);
            return dataType.Substring(1);
        }

        string ExtractLabelName(out string newLine, string line)
        {
            newLine = line;
            var labelName = SeparateLine(line)[0];
            if (labelName==null || !labelName.EndsWith(":")) return null;

            var dp = line.IndexOf(":");
            newLine = line.Substring(dp + 1).Trim();

            return labelName.Substring(0, dp);
        }

        string[] SeparateLine(string line)
        {
            return line.Split(' ');
        }
    }
}