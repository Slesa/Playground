using System;
using System.Collections.Generic;
using System.Linq;
using NetDLX.Core.Exceptions;

namespace NetDLX.Core
{
    public static class AssembleBase
    {
        public static bool CanAssemble(string command, IEnumerable<CpuOperation> knownCommands)
        {
            var found = knownCommands.Count(op => op.Mnemonic == command.ToUpper());
            return found > 0;
        }

        public static UInt32 Assemble(string command, string[] args, IEnumerable<CpuOperation> knownCommands)
        {
            var query = from o in knownCommands where o.Mnemonic == command.ToUpper() select o;
            var op = query.FirstOrDefault();
            if (op == null)
                throw new UnknownCommandException();

            if (args == null || args.Count() < op.Operands.Length)
                throw new SyntaxErrorException();

            var opcode = (UInt32)(op.OpCode << 26);
            if (op.Operands.Length > 1)
            {
                var op1 = TranslateOperands.Translate(op.Operands[0], args[0]);
                opcode |= op1 << 21;
            }
            if (op.Operands.Length > 2)
            {
                var op2 = TranslateOperands.Translate(op.Operands[1], args[1]);
                opcode |= op2 << 16;
            }

            return opcode;
        }

    }
}