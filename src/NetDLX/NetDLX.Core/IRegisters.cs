using System;

namespace NetDLX.Core
{
    public interface IRegisters
    {
        uint NumberOfRegisters { get; }
        uint NumberOfFloatRegisters { get; }

        UInt32 ReadWord(uint register);
        void WriteWord(uint register, UInt32 value);

        UInt16 ReadHalfWord(uint register);
        void WriteHalfWord(uint register, UInt16 value);

        Byte ReadByte(uint register);
        void WriteByte(uint register, Byte value);

        float ReadFloat(uint register);
        void WriteFloat(uint register, float value);

        double ReadDouble(uint register);
        void WriteDouble(uint register, double value);

        UInt32 ReadFWord(uint register);
        void WriteFWord(uint register, UInt32 value);
    }
}