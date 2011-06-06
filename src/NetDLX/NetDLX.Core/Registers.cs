using System;
using System.Runtime.InteropServices;
using NetDLX.Core.Exceptions;

namespace NetDLX.Core
{
    [StructLayout(LayoutKind.Explicit)]
    struct MathRegister
    {
        [FieldOffset(0)] public float f;
        [FieldOffset(0)] public double d;
        [FieldOffset(0)] public UInt32 w;
    }

    public class Registers : IRegisters
    {
        public uint NumberOfRegisters { get; private set; }
        public uint NumberOfFloatRegisters { get; private set; }
        
        public Registers()
        {
            NumberOfRegisters = 32;
            R = new UInt32[NumberOfRegisters];

            NumberOfFloatRegisters = 32;
            FP = new MathRegister[NumberOfFloatRegisters];
        }

        public UInt32 ReadWord(uint register)
        {
            if (register >= NumberOfRegisters)
                throw new BoundaryException();
            return R[register];
        }

        public void WriteWord(uint register, UInt32 value)
        {
            if (register==0 || register >= NumberOfRegisters)
                throw new BoundaryException();
            R[register] = value;
        }

        public UInt16 ReadHalfWord(uint register)
        {
            if (register >= NumberOfRegisters)
                throw new BoundaryException();
            return (UInt16) R[register];
        }

        public void WriteHalfWord(uint register, UInt16 value)
        {
            if (register==0 || register >= NumberOfRegisters)
                throw new BoundaryException();
            R[register] = value;
        }

        public Byte ReadByte(uint register)
        {
            if (register >= NumberOfRegisters)
                throw new BoundaryException();
            return (Byte) R[register];
        }

        public void WriteByte(uint register, Byte value)
        {
            if (register==0 || register >= NumberOfRegisters)
                throw new BoundaryException();
            R[register] = value;
        }

        public float ReadFloat(uint register)
        {
            if (register >= NumberOfFloatRegisters)
                throw new BoundaryException();
            return FP[register].f;
        }

        public void WriteFloat(uint register, float value)
        {
            if (register >= NumberOfFloatRegisters)
                throw new BoundaryException();
            FP[register].f = value;
        }

        public double ReadDouble(uint register)
        {
            if (register*2 >= NumberOfFloatRegisters)
                throw new BoundaryException();
            return FP[register*2].d;
        }

        public void WriteDouble(uint register, double value)
        {
            if (register*2 >= NumberOfFloatRegisters)
                throw new BoundaryException();
            FP[register*2].d = value;
        }

        public UInt32 ReadFWord(uint register)
        {
            if (register >= NumberOfFloatRegisters)
                throw new BoundaryException();
            return FP[register].w;
        }

        public void WriteFWord(uint register, UInt32 value)
        {
            if (register >= NumberOfFloatRegisters)
                throw new BoundaryException();
            FP[register].w = value;
        }

        UInt32[] R; // Main Registers, R0 is always zero
        MathRegister[] FP;
    }
}