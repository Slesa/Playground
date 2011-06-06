using System;
using System.Collections.Generic;
using System.Linq;
using NetDLX.Core.Exceptions;

namespace NetDLX.Core
{
    public class AssembleData : IAssemble
    {
        struct OpCode
        {
            public string Mnemonic;
            public Byte Command;
            public uint Operands;
        }

        IEnumerable<OpCode> KnownCommands = new List<OpCode>
            {
                new OpCode {Mnemonic = "LB", Command = 0x10, Operands = 2},
            };
        Dictionary<string, Byte> Commands = new Dictionary<string, byte>
            {
                {"LB",   0x10},          // 010000
                {"LBU",  0x11},          // 010001
                {"LH",   0x12},          // 010010
                {"LHU",  0x13},          // 010011
                {"LW",   0x14},          // 010100
                {"LF",   0x16},          // 010110
                {"LD",   0x17},          // 010111
                {"SB",   0x18},          // 011000
                {"SH",   0x1A},          // 011010
                {"SW",   0x1C},          // 011100
                {"SF",   0x1E},          // 011110
                {"SD",   0x1F},          // 011111
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
            
            if( args.Count()<op.Operands )
                throw new SyntaxErrorException();

            var op1 = AssembleOperands.TranslateGpRegister(args[0]);
            var op2 = AssembleOperands.TranslateGpRegister(args[1]);

            return (UInt32)(op.Command << 26);
            /*            if(op==null)
                throw new UnknownCommandException();
            if( args.Count() !=op. )
            Byte opCode;
            if( !Commands.TryGetValue(command, out opCode) )
            if( !)
            }? 0u : opCode */
        }
    }
}