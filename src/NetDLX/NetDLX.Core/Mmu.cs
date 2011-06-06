using System;
using NetDLX.Core.Exceptions;

namespace NetDLX.Core
{
    public class Mmu : IMmu
    {
        public Mmu()
        {
            Size = 32768;
            _content = new Byte[Size];
        }
        /*
        public Mmu(uint size)
        {
            Size = size;
            _content = new UInt32[size];
        }*/

        public uint Size { get; private set; }

        public UInt32 ReadWord(uint address)
        {
            if( address+3>=Size )
                throw new BoundaryException();
            return (UInt32) (_content[address]) + (UInt32) (_content[address + 1] << 8)
                + (UInt32) (_content[address + 2] << 16) + (UInt32) (_content[address + 3] << 24);
        }

        public void WriteWord(uint address, UInt32 value)
        {
            if( address+3>=Size )
                throw new BoundaryException();
            _content[address] = (Byte) (value & 0xFF);
            _content[address + 1] = (Byte) ((value >> 8) & 0xFF);
            _content[address + 2] = (Byte) ((value >> 16) & 0xFF);
            _content[address + 3] = (Byte) ((value >> 24) & 0xFF);
        }

        public UInt16 ReadHalfWord(uint address)
        {
            if( address+1>=Size )
                throw new BoundaryException();
            return  (UInt16)( (_content[address]) + (UInt16)(_content[address + 1] << 8));
        }

        public void WriteHalfWord(uint address, UInt16 value)
        {
            if( address+1>=Size )
                throw new BoundaryException();
            _content[address] = (Byte) (value & 0xFF);
            _content[address + 1] = (Byte) ((value >> 8) & 0xFF);
        }

        public Byte ReadByte(uint address)
        {
            if( address>=Size )
                throw new BoundaryException();
            return _content[address];
        }

        public void WriteByte(uint address, Byte value)
        {
            if( address>=Size )
                throw new BoundaryException();
            _content[address] = value;
        }

        readonly Byte[] _content;
    }
}