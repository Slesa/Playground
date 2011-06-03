using System;

namespace NetDLX.Core
{
    public interface IMmu
    {
        uint Size { get; }

        UInt32 ReadWord(uint address);
        void WriteWord(uint address, UInt32 value);

        UInt16 ReadHalfWord(uint address);
        void WriteHalfWord(uint address, UInt16 value);

        Byte ReadByte(uint address);
        void WriteByte(uint address, Byte value);
    }
}