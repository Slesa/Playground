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
                new Operation { Mnemonic="ADDD", OpCode = 0x23, Operands = "DDD"},
                new Operation { Mnemonic="ADDF", OpCode = 0x22, Operands = "FFF"},
                new Operation { Mnemonic="SUBD", OpCode = 0x27, Operands = "DDD"},
                new Operation { Mnemonic="SUBF", OpCode = 0x26, Operands = "FFF"},
                new Operation { Mnemonic="MULTD", OpCode = 0x2B, Operands = "DDD"},
                new Operation { Mnemonic="MULTF", OpCode = 0x2A, Operands = "FFF"},
                new Operation { Mnemonic="DIVD", OpCode = 0x2F, Operands = "DDD"},
                new Operation { Mnemonic="DIVF", OpCode = 0x2E, Operands = "FFF"},
                new Operation { Mnemonic="MOVD", OpCode = 0x05, Operands = "FF"},
                new Operation { Mnemonic="MOVF", OpCode = 0x04, Operands = "DD"},
                new Operation { Mnemonic="CVTF2D", OpCode = 0x08, Operands = "DF"},
                new Operation { Mnemonic="CVTF2I", OpCode = 0x09, Operands = "IF"},
                new Operation { Mnemonic="CVTD2F", OpCode = 0x0A, Operands = "FD"},
                new Operation { Mnemonic="CVTD2I", OpCode = 0x0B, Operands = "ID"},
                new Operation { Mnemonic="CVTI2F", OpCode = 0x0C, Operands = "FI"},
                new Operation { Mnemonic="CVTI2D", OpCode = 0x0D, Operands = "DI"},

            };

        // ADDD    100011 8C 23   S2isB,   ERROR, T_ALU,       4,  "DDD",
        // ADDF    100010 88 22   S2isB,   ERROR, T_ALU,       4,  "FFF",
        // SUBD    100111 9C 27   S2isB,   ERROR, T_ALU,       4,  "DDD",
        // SUBF    100110 98 26   S2isB,   ERROR, T_ALU,       4,  "FFF",
        // MULTD   101011 AC 2B   S2isB,   ERROR, T_ALU,       4,  "DDD",
        // MULTF   101010 A8 2A   S2isB,   ERROR, T_ALU,       4,  "FFF",
        // DIVD    101111 BC 2F   S2isB,   ERROR, T_ALU,       4,  "DDD",
        // DIVF    101110 B8 2E   S2isB,   ERROR, T_ALU,       4,  "FFF",        
        // MOVF    000100 10 04   MOVFP,   ERROR, T_Move,      4,  "FF ",
        // MOVD    000101 14 05   MOVFP,   ERROR, T_Move,      4,  "DD ",
        // CVTF2D  001010 20 08   CVT,     ERROR, T_Convert,   4,  "DF ",
        // CVTF2I  001011 24 09   CVT,     ERROR, T_Convert,   4,  "IF ",
        // CVTD2F  001100 28 0A   CVT,     ERROR, T_Convert,   4,  "FD ",
        // CVTD2I  001101 2C 0B   CVT,     ERROR, T_Convert,   4,  "ID ",
        // CVTI2F  001110 30 0C   CVT,     ERROR, T_Convert,   4,  "FI ",
        // CVTI2D  001111 34 0D   CVT,     ERROR, T_Convert,   4,  "DI ",

        //"ADDD ",    "FFFd12", 'R', 0xFC000023,   // 1111 1100  10 0011
        //"ADDF ",    "fffd12", 'R', 0xFC000022,   // 1111 1100  10 0010
        //"SUBD ",    "FFFd12", 'R', 0xFC000027,   // 1111 1100  10 0111
        //"SUBF ",    "fffd12", 'R', 0xFC000026,   // 1111 1100  10 0110
        //"MULTD ",   "FFFd12", 'R', 0xFC00002B,   // 1111 1100  10 1011
        //"MULTF ",   "fffd12", 'R', 0xFC00002A,   // 1111 1100  10 1010
        //"DIVD ",    "FFFd12", 'R', 0xFC00002F,   // 1111 1100  10 1111
        //"DIVF ",    "fffd12", 'R', 0xFC00002E,   // 1111 1100  10 1110
        //"MOVD ",    "FF d1 ", 'R', 0xFC000005,   // 1111 1100  00 0101
        //"MOVF ",    "ff d1 ", 'R', 0xFC000004,   // 1111 1100  00 0100
        //"CVTD2F ",  "fF d1 ", 'R', 0xFC00000A,   // 1111 1100  00 1010
        //"CVTD2I ",  "fF d1 ", 'R', 0xFC00000B,   // 1111 1100  00 1011
        //"CVTF2D ",  "Ff d1 ", 'R', 0xFC000008,   // 1111 1100  00 1000
        //"CVTF2I ",  "ff d1 ", 'R', 0xFC000009,   // 1111 1100  00 1001
        //"CVTI2D ",  "Ff d1 ", 'R', 0xFC00000D,   // 1111 1100  00 1101
        //"CVTI2F ",  "ff d1 ", 'R', 0xFC00000C,   // 1111 1100  00 1100
    }

}