using System;
using System.Collections.Generic;
using System.Linq;
using NetDLX.Core.Exceptions;

namespace NetDLX.Core
{
    public class AssembleData : AssembleBase, IAssemble
    {
        class Command
        {
            public string Mnemonic { get; set; }
            public Byte OpCode { get; set; }
            public string Operands { get; set; }
        }

        IEnumerable<Command> KnownCommands = new List<Command>
            {
                new Command {Mnemonic = "LB", OpCode = 0x10, Operands = "RR"},          // 010000
                new Command {Mnemonic = "LBU", OpCode = 0x11, Operands = "RR"},         // 010001
                new Command {Mnemonic = "LH", OpCode = 0x12, Operands = "RR"},          // 010010
                new Command {Mnemonic = "LHU", OpCode = 0x13, Operands = "RR"},         // 010011
                new Command {Mnemonic = "LW", OpCode = 0x14, Operands = "RR"},          // 010100
                new Command {Mnemonic = "LF", OpCode = 0x16, Operands = "RR"},          // 010110
                new Command {Mnemonic = "LD", OpCode = 0x17, Operands = "RR"},          // 010111
                new Command {Mnemonic = "SB", OpCode = 0x18, Operands = "RR"},          // 011000
                new Command {Mnemonic = "SH", OpCode = 0x1A, Operands = "RR"},          // 011010
                new Command {Mnemonic = "SW", OpCode = 0x1C, Operands = "RR"},          // 011100
                new Command {Mnemonic = "SF", OpCode = 0x1E, Operands = "RR"},          // 011110
                new Command {Mnemonic = "SD", OpCode = 0x1F, Operands = "RR"},          // 011111
            };

        public bool CanAssemble(string command)
        {
            var found = KnownCommands.Count(op => op.Mnemonic == command.ToUpper());
            return found > 0;
        }

        public UInt32 Translate(string command, string[] args)
        {
            var query = from o in KnownCommands where o.Mnemonic == command.ToUpper() select o;
            var op = query.FirstOrDefault();
            if( op==null )
                throw new UnknownCommandException();
            
            if( args==null || args.Count()<op.Operands.Length )
                throw new SyntaxErrorException();

            var opcode = (UInt32)(op.OpCode<<26);
            if (op.Operands.Length > 1)
            {
                var op1 = TranslateRegister(op.Operands[0], args[0]);
                opcode |= op1 << 21;
            }
            if (op.Operands.Length > 2)
            {
                var op2 = TranslateRegister(op.Operands[1], args[1]);
                opcode |= op2 << 16;
            }

            return opcode;
        }


    }
}