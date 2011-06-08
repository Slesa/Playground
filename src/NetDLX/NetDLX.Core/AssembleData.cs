using System;
using System.Collections.Generic;

namespace NetDLX.Core
{
    public class AssembleData : IAssemble
    {
        IEnumerable<CpuOperation> KnownCommands = new List<CpuOperation>
            {
                new CpuOperation {Mnemonic = "LB", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "LBU", OpCode = 0x11, Operands = "RR"},         // 010001
                new CpuOperation {Mnemonic = "SB", OpCode = 0x18, Operands = "RR"},          // 011000
                new CpuOperation {Mnemonic = "LH", OpCode = 0x12, Operands = "RR"},          // 010010
                new CpuOperation {Mnemonic = "LHU", OpCode = 0x13, Operands = "RR"},         // 010011
                new CpuOperation {Mnemonic = "SH", OpCode = 0x1A, Operands = "RR"},          // 011010
                new CpuOperation {Mnemonic = "LW", OpCode = 0x14, Operands = "RR"},          // 010100
                new CpuOperation {Mnemonic = "SW", OpCode = 0x1C, Operands = "RR"},          // 011100
                new CpuOperation {Mnemonic = "LF", OpCode = 0x16, Operands = "RR"},          // 010110
                new CpuOperation {Mnemonic = "LD", OpCode = 0x17, Operands = "RR"},          // 010111
                new CpuOperation {Mnemonic = "SF", OpCode = 0x1E, Operands = "RR"},          // 011110
                new CpuOperation {Mnemonic = "SD", OpCode = 0x1F, Operands = "RR"},          // 011111
            };

        public bool CanAssemble(string command)
        {
            return AssembleBase.CanAssemble(command, KnownCommands);
        }

        public UInt32 Assemble(string command, string[] args)
        {
            return AssembleBase.Assemble(command, args, KnownCommands);
        }
    }
}