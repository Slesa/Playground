using System;

namespace NetDLX.Code
{
    public enum LabelType
    {
        JUMP,
        BYTE,
        HALFWORD,
        WORD,
        STRING,
    }

    public class Label
    {
        public string Name { get; set; }
        public int Address { get; set; }
        public int Size
        {
            get
            {
                switch(Type)
                {
                    case LabelType.BYTE:
                        return 1;
                    case LabelType.HALFWORD:
                        return 2;
                    case LabelType.WORD:
                        return 4;
                }
                return 0;
            }
        }
        public LabelType Type { get; set; }
        public string Value { get; set; }
        public uint GetWord()
        {
            var value = Value;
            var style = System.Globalization.NumberStyles.Number;
            if( value.StartsWith("0x") )
            {
                style = System.Globalization.NumberStyles.HexNumber;
                value = value.Substring(2);
            }
            return uint.Parse(value, style);
        }
        public T GetValue<T>()
        {
            return (T) Convert.ChangeType(Value, typeof(T));
        }
    }
}
