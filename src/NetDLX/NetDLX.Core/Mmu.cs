using System;

namespace NetDLX.Core
{
    public class Mmu : IMmu
    {
        public Mmu(uint size=32768u)
        {
            Size = size;
            _content = new UInt32[size];
        }

        public uint Size { get; private set; }

        public UInt32 ReadWord(uint address)
        {
            return 0;
        }

        public void WriteWord(uint address, UInt32 word)
        {
        }

        public UInt16 ReadHalfWord(uint address)
        {
            return 0;
        }

        public void WriteHalfWord(uint address, UInt16 hword)
        {
        }

        public Byte ReadByte(uint adress)
        {
            return 0;
        }

        public void WriteByte(uint address, Byte @byte)
        {
        }

        int _size;
        UInt32[] _content;
    }
}