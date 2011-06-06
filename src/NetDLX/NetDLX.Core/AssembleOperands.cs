using System;
using System.Collections.Generic;

namespace NetDLX.Core
{
    public class AssembleOperands 
    {
        Dictionary<string, Byte> Registers = new Dictionary<string, byte>
            {
                {"r0", 0x00},  // 00000
                {"r1", 0x01},  // 00001
                {"r2", 0x02},  // 00010
                {"r3", 0x03},  // 00011
                {"r4", 0x04},  // 00100
                {"r5", 0x05},  // 00101
                {"r6", 0x06},  // 00110
                {"r7", 0x07},  // 00111
                {"r8", 0x08},  // 01000
                {"r9", 0x09},  // 01001
                {"r10", 0x0A}, // 01010
                {"r11", 0x0B}, // 01011
                {"r12", 0x0B}, // 01011
            };

        public static uint TranslateGpRegister(string register)
        {
            var reg = register.ToLower();
            if (reg.StartsWith("r"))
            {
                reg = reg.Substring(1);
                uint value;
                if( UInt32.TryParse(reg, out value) )
                {
                    if (value < 32)
                        return value;
                }
            }
            return 0;
        }

        public static uint TranslateFpRegister(string register)
        {
            var reg = register.ToLower();
            if (reg.StartsWith("f"))
            {
                reg = reg.Substring(1);
                uint value;
                if( UInt32.TryParse(reg, out value) )
                {
                    if (value < 32)
                        return value;
                }
            }
            return 0;
        }
    }
}