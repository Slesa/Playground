using System;
using System.Collections.Generic;

namespace NetDLX.Core
{
    public class AssembleArithmetic : IAssemble
    {
        IEnumerable<CpuOperation> KnownCommands = new List<CpuOperation>
            {
                new CpuOperation {Mnemonic = "ADD", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "ADDI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "ADDU", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "ADDUI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SUB", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SUBI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SUBU", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SUBUI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "MULT", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "MULTU", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "DIV", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "DIVU", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "AND", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "ANDI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "OR", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "ORI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "XOR", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "XORI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "LHI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SLL", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SRL", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SRA", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SRAI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SLLI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SRLI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SLT", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SLTI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SGT", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SGTI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SLE", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SLEI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SGE", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SGEI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SEQ", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SEQI", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SNE", OpCode = 0x10, Operands = "RR"},          // 010000
                new CpuOperation {Mnemonic = "SNEI", OpCode = 0x10, Operands = "RR"},          // 010000
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