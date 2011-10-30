using System.Linq;
using NetDLX.Core;

namespace NetDLX.Code
{
    public class FpuOpBuilder : BuilderHandlerBase, IBuilderHandler
    {
        public bool CanHandle(string sourceLine)
        {
            var line = NormalizeSpaces(sourceLine);
            if (IsComment(line)) return false;
            var operation = ExtractOpCode(out line, line);
            return operation != null;
        }

        public string Handle(Program program, string sourceLine)
        {
            if (!CanHandle(sourceLine)) return sourceLine;

            var line = NormalizeSpaces(sourceLine);
            var operation = ExtractOpCode(out line, line);

            var opcode = ((uint) operation.OpCode) << 26;
            var operands = operation.Operands;
            var arguments = line.Split(',');
            var currentArg = 0;
            var offset = 21;
            foreach(var operand in operands)
            {
                var code = TranslateOperands.Translate(operand, arguments[currentArg++].Trim());
                opcode += code << offset;
                offset -= 5;
            }
            program.AddCode(opcode);
            return null;
        }

        static Operation ExtractOpCode(out string newLine, string line)
        {
            newLine = line;
            var mnemonic = SeparateLine(line)[0];
            if (mnemonic == null || IsLabel(mnemonic)) return null;

            var query = from op in FpuOperations.KnownOps where op.Mnemonic.Equals(mnemonic.ToUpper()) select op;
            var opcode = query.FirstOrDefault();
            if (opcode == null) return null;

            newLine = line.Replace(mnemonic, "").Trim();

            return opcode;
        }

    }
}