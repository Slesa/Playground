using System;

namespace NetDLX.Code
{
    public class LabelBuilder : BuilderHandlerBase, IBuilderHandler
    {
        public bool CanHandle(string sourceLine)
        {
            var line = NormalizeSpaces(sourceLine);
            if (IsComment(line)) return false;
            var labelName = ExtractLabelName(out line, line);
            return labelName != null;
        }

        public string Handle(Program program, string sourceLine)
        {
            if (!CanHandle(sourceLine)) return sourceLine;

            var line = NormalizeSpaces(sourceLine);
            var labelName = ExtractLabelName(out line, line);
            var labelType = ExtractLabelType(out line, line);
            Label label;
            if (labelType == null)
            {
                label = new Label { Name = labelName, Type = LabelType.JUMP };
                program.AddLabel(label);
                return line;
            }

            LabelType type;
            if (!Enum.TryParse(labelType, out type))
                return line;
            var labelValue = ExtractLabelValue(out line, line);
            label = new Label { Name = labelName, Type = type, Value = labelValue };
            program.AddLabel(label);
            return line;
        }

        static string ExtractLabelName(out string newLine, string line)
        {
            newLine = line;
            var labelName = SeparateLine(line)[0];
            if (labelName == null || !labelName.EndsWith(":")) return null;

            var dp = line.IndexOf(":");
            newLine = line.Substring(dp + 1).Trim();

            return labelName.Substring(0, dp);
        }

        static string ExtractLabelType(out string newLine, string line)
        {
            newLine = line;
            if (String.IsNullOrEmpty(line)) return null;
            var separation = SeparateLine(line);
            var dataType = separation[0].ToUpper();
            if (!dataType.StartsWith("."))
                return null;

            newLine = line.Substring(dataType.Length + 1);
            return dataType.Substring(1);
        }

        static string ExtractLabelValue(out string newLine, string line)
        {
            newLine = line;
            if (String.IsNullOrEmpty(line)) return null;
            var dataValue = SeparateLine(line)[0];
            newLine = null;
            return dataValue;
        }

    }

}