using System;
using System.Globalization;
using NetDLX.Core.Exceptions;

namespace NetDLX.Code
{
    public static class TranslateOperands
    {
        public static uint Translate(char shorten, string specific)
        {
            if (shorten == 'R')
                return TranslateGpRegister(specific);
            if (shorten == 'F')
                return TranslateFpRegister(specific);
            if (shorten == 'D')
                return TranslateDpRegister(specific);
            if (shorten == 'I')
                return TranslateImmediate(specific);
            throw new InvalidRegisterException();
        }
        
        static uint TranslateImmediate(string specific)
        {
            return specific.StartsWith("0x") 
                ? uint.Parse(specific.Substring(2), NumberStyles.HexNumber) 
                : ExtractWord(specific);
        }

        static uint TranslateGpRegister(string register)
        {
            var reg = register.ToLower();
            if (!reg.StartsWith("r"))
                throw new SyntaxErrorException();
            var id = ExtractWord(reg.Substring(1));
            if (id >= 32)
                throw new InvalidRegisterException();
            return id;
        }

        static uint TranslateFpRegister(string register)
        {
            var reg = register.ToLower();
            if (!reg.StartsWith("f"))
                throw new SyntaxErrorException();
            var id = ExtractWord(reg.Substring(1));
            if (id >= 32)
                throw new InvalidRegisterException();
            return id;
        }

        static uint TranslateDpRegister(string register)
        {
            var reg = register.ToLower();
            if (!reg.StartsWith("f"))
                throw new SyntaxErrorException();
            var id = ExtractWord(reg.Substring(1));
            if ((id % 2 != 0) || (id >= 32))
                throw new InvalidRegisterException();
            return id;
        }

        static uint ExtractWord(string reg)
        {
            uint value;
            if (!UInt32.TryParse(reg, out value))
                throw new SyntaxErrorException();
            return value;
        }
    }
}