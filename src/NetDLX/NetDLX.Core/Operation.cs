using System;
using System.Collections.Generic;

namespace NetDLX.Core
{
    public class Operation
    {
        public string Mnemonic { get; set; }
        public Byte OpCode { get; set; }
        public string Operands { get; set; }
    }

    public static class FpuOperations
    {
        public static readonly IEnumerable<Operation> KnownOps = new List<Operation>
            {
                new Operation() { Mnemonic="ADDD", OpCode = 0x23, Operands = "DDD"},
                new Operation() { Mnemonic="ADDF", OpCode = 0x22, Operands = "FFF"},
                new Operation() { Mnemonic="SUBD", OpCode = 0x27, Operands = "DDD"},
                new Operation() { Mnemonic="SUBF", OpCode = 0x26, Operands = "FFF"},
                new Operation() { Mnemonic="MULTD", OpCode = 0x2B, Operands = "DDD"},
                new Operation() { Mnemonic="MULTF", OpCode = 0x2A, Operands = "FFF"},
                new Operation() { Mnemonic="DIVD", OpCode = 0x2F, Operands = "DDD"},
                new Operation() { Mnemonic="DIVF", OpCode = 0x2E, Operands = "FFF"},

            };

        // ADDD    100011 8C 23   S2isB,   ERROR, T_ALU,       4,  "DDD",
        // ADDF    100010 88 22   S2isB,   ERROR, T_ALU,       4,  "FFF",
        // SUBD    100111 9C 27   S2isB,   ERROR, T_ALU,       4,  "DDD",
        // SUBF    100110 98 26   S2isB,   ERROR, T_ALU,       4,  "FFF",
        // MULTD   101011 AC 2B   S2isB,   ERROR, T_ALU,       4,  "DDD",
        // MULTF   101010 A8 2A   S2isB,   ERROR, T_ALU,       4,  "FFF",
        // DIVD    101111 BC 2F   S2isB,   ERROR, T_ALU,       4,  "DDD",
        // DIVF    101110 B8 2E   S2isB,   ERROR, T_ALU,       4,  "FFF",
        
        //"ADDD ",    "FFFd12", 'R', 0xFC000023,   // 1111 1100  10 0011
        //"ADDF ",    "fffd12", 'R', 0xFC000022,   // 1111 1100  10 0010
        //"SUBD ",    "FFFd12", 'R', 0xFC000027,   // 1111 1100  10 0111
        //"SUBF ",    "fffd12", 'R', 0xFC000026,   // 1111 1100  10 0110
        //"MULTD ",   "FFFd12", 'R', 0xFC00002B,   // 1111 1100  10 1011
        //"MULTF ",   "fffd12", 'R', 0xFC00002A,   // 1111 1100  10 1010
        //"DIVD ",    "FFFd12", 'R', 0xFC00002F,   // 1111 1100  10 1111
        //"DIVF ",    "fffd12", 'R', 0xFC00002E,   // 1111 1100  10 1110
    }

}