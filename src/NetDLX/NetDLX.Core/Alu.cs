using System;

namespace NetDLX.Core
{
    public class Alu
    {
        public UInt32 Add(UInt32 a, UInt32 b)
        {
            return a+b;
        }

        public UInt32 Sub(UInt32 a, UInt32 b)
        {
            return a-b;
        }

        public float Mult(float a, float b)
        {
            return a*b;
        }

        public float Div(float a, float b)
        {
            return a/b;
        }

        public double Mult(double a, double b)
        {
            return a*b;
        }

        public double Div(double a, double b)
        {
            return a/b;
        }

        public UInt32 And(UInt32 a, UInt32 b)
        {
            return a&b;
        }

        public UInt32 Or(UInt32 a, UInt32 b)
        {
            return a|b;
        }

        public UInt32 Xor(UInt32 a, UInt32 b)
        {
            return a^b;
        }

        public UInt32 Lhh(UInt16 value)
        {
            return 0;
        }

        public UInt32 ShiftLeft(UInt32 value)
        {
            return 0;
        }

        public UInt32 ShiftLogicLeft(UInt32 value)
        {
            return 0;
        }

        public UInt32 ShiftRight(UInt32 value)
        {
            return 0;
        }

        public UInt32 ShiftLogicRight(UInt32 value)
        {
            return 0;
        }
    }
}