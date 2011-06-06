using System;
using Machine.Fakes;
using Machine.Specifications;
using NetDLX.Core.Exceptions;

namespace NetDLX.Core.Specs
{
    [Subject(typeof(Registers))]
    public class When_accessing_registers_after_construction : WithSubject<Registers>
    {
        Establish _context = () =>
            {
                _words = new UInt32[Subject.NumberOfRegisters];
                _floats = new float[Subject.NumberOfFloatRegisters];
            };

        Because of = () =>
            {
                for(uint i=0; i<Subject.NumberOfRegisters; i++)
                    _words[i] = Subject.ReadWord(i);
                for (uint f = 0; f < Subject.NumberOfFloatRegisters; f++)
                    _floats[f] = Subject.ReadFloat(f);
            };

        It should_have_zero_words = () => _words.ShouldEachConformTo(x => x == 0);
        It should_have_zero_floats = () => _floats.ShouldEachConformTo(x => x == 0f);

        static UInt32[] _words;
        static float[] _floats;
    }

    [Subject(typeof(Registers))]
    public class When_reading_registers_after_writing : WithSubject<Registers>
    {
        Establish context = () =>
            {
                Subject.WriteWord(10, 0x12345678);
                Subject.WriteHalfWord(12, 0x1234);
                Subject.WriteByte(14, 0x12);

                Subject.WriteFloat(10, 42.1234f);
                Subject.WriteDouble(12, 42.9876);
                Subject.WriteFWord(14, 0xFEDCBA98);
            };

        Because of = () =>
            {
                _word = Subject.ReadWord(10);
                _halfWord = Subject.ReadHalfWord(12);
                _byte = Subject.ReadByte(14);

                _float = Subject.ReadFloat(10);
                _double = Subject.ReadDouble(12);
                _fWord = Subject.ReadFWord(14);
            };

        It should_read_word_value = () => _word.ShouldEqual(0x12345678u);
        It should_read_half_word_value = () => _halfWord.ShouldEqual((UInt16)0x1234);
        It should_read_byte_value = () => _byte.ShouldEqual((Byte) 0x12);

        It should_read_float_value = () => _float.ShouldBeCloseTo(42.1234f, 0.01f);
        It should_read_double_value = () => _double.ShouldBeCloseTo(42.9876, 0.01);
        It should_read_floating_word = () => _fWord.ShouldEqual(0xFEDCBA98);

        static UInt32 _word;
        static UInt16 _halfWord;
        static Byte _byte;
        static float _float;
        static double _double;
        static UInt32 _fWord;
    }

    [Subject(typeof(Mmu))]
    public class When_reading_same_register_in_different_bits : WithSubject<Registers>
    {
        Establish context = () =>
            {
                Subject.WriteWord(register, 0xFEDBCA98);
                Subject.WriteDouble(register, 42.123456789);
            };

        Because of = () =>
            {
                _halfword = Subject.ReadHalfWord(register);
                _byte = Subject.ReadByte(register);

                _float = Subject.ReadFloat(register*2);
                _fWord = Subject.ReadFWord(register);
            };

        It should_read_half_word_value = () => _halfword.ShouldEqual((UInt16) 0xCA98);
        It should_read_byte_value = () => _byte.ShouldEqual((Byte) 0x98);

        // TODO: How to compare?
        //It should_read_float_value = () => _float.ShouldBeCloseTo(42.12345f, 0.01f);
        //It should_read_floating_word_value = () => _fWord.ShouldEqual(42u);:w

        const uint register = 10;
        static UInt16 _halfword;
        static Byte _byte;
        static float _float;
        static UInt32 _fWord;
    }

    [Subject(typeof(Mmu))]
    public class When_reading_exceeds_register_count : WithSubject<Registers>
    {
        Because of = () =>
        {
            _wordError = Catch.Exception(() => Subject.ReadWord(Subject.NumberOfRegisters));
            _halfError = Catch.Exception(() => Subject.ReadHalfWord(Subject.NumberOfRegisters));
            _byteError = Catch.Exception(() => Subject.ReadByte(Subject.NumberOfRegisters));

            _floatError = Catch.Exception(() => Subject.ReadFloat(Subject.NumberOfFloatRegisters));
            _doubleError = Catch.Exception(() => Subject.ReadDouble(Subject.NumberOfFloatRegisters/2));
            _fWordError = Catch.Exception(() => Subject.ReadFWord(Subject.NumberOfFloatRegisters));
        };

        It should_fail_for_words = () => _wordError.ShouldBeOfType(typeof(BoundaryException));
        It should_fail_for_half_words = () => _halfError.ShouldBeOfType(typeof(BoundaryException));
        It should_fail_for_bytes = () => _byteError.ShouldBeOfType(typeof(BoundaryException));

        It should_fail_for_floats = () => _floatError.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_for_doubles = () => _doubleError.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_for_floating_words = () => _fWordError.ShouldBeOfType(typeof (BoundaryException));

        static Exception _wordError;
        static Exception _halfError;
        static Exception _byteError;
        static Exception _floatError;
        static Exception _doubleError;
        static Exception _fWordError;
    }

    [Subject(typeof(Mmu))]
    public class When_writing_exceeds_register_count : WithSubject<Registers>
    {
        Because of = () =>
        {
            _word0Error = Catch.Exception(() => Subject.WriteWord(0, 1));
            _wordError = Catch.Exception(() => Subject.WriteWord(Subject.NumberOfRegisters, 1));
            _half0Error = Catch.Exception(() => Subject.WriteHalfWord(0, 1));
            _halfError = Catch.Exception(() => Subject.WriteHalfWord(Subject.NumberOfRegisters, 1));
            _byte0Error = Catch.Exception(() => Subject.WriteByte(0, 1));
            _byteError = Catch.Exception(() => Subject.WriteByte(Subject.NumberOfRegisters, 1));

            _floatError = Catch.Exception(() => Subject.WriteFloat(Subject.NumberOfFloatRegisters, 1));
            _doubleError = Catch.Exception(() => Subject.WriteDouble(Subject.NumberOfFloatRegisters/2, 1));
            _fWordError = Catch.Exception(() => Subject.WriteFWord(Subject.NumberOfFloatRegisters, 1));
        };

        It should_fail_for_word_at_register_zero = () => _word0Error.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_for_words = () => _wordError.ShouldBeOfType(typeof(BoundaryException));
        It should_fail_for_half_word_at_register_zero = () => _half0Error.ShouldBeOfType(typeof(BoundaryException));
        It should_fail_for_half_words = () => _halfError.ShouldBeOfType(typeof(BoundaryException));
        It should_fail_for_byteat_register_zero = () => _byte0Error.ShouldBeOfType(typeof(BoundaryException));
        It should_fail_for_bytes = () => _byteError.ShouldBeOfType(typeof(BoundaryException));

        It should_fail_for_floats = () => _floatError.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_for_doubles = () => _doubleError.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_for_floating_words = () => _fWordError.ShouldBeOfType(typeof (BoundaryException));

        static Exception _word0Error;
        static Exception _wordError;
        static Exception _half0Error;
        static Exception _halfError;
        static Exception _byte0Error;
        static Exception _byteError;
        static Exception _floatError;
        static Exception _doubleError;
        static Exception _fWordError;
    }
}