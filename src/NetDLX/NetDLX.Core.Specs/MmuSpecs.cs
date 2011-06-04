using System;
using Machine.Fakes;
using Machine.Specifications;

namespace NetDLX.Core.Specs
{
    [Subject(typeof(Mmu))]
    public class When_accessing_memory_after_construction : WithSubject<Mmu>
    {
        Because of = () =>
            {
                _wordBottom = Subject.ReadWord(0);
                _halfBottom = Subject.ReadHalfWord(0);
                _byteBottom = Subject.ReadByte(0);

                _wordMiddle = Subject.ReadWord(Subject.Size/2);
                _halfMiddle = Subject.ReadHalfWord(Subject.Size/2);
                _byteMiddle = Subject.ReadByte(Subject.Size/2);

                _wordTop = Subject.ReadWord(Subject.Size - 4);
                _halfTop = Subject.ReadHalfWord(Subject.Size - 2);
                _byteTop = Subject.ReadByte(Subject.Size - 1);
            };

        It should_have_zero_word_at_bottom = () => _wordBottom.ShouldEqual(0u);
        It should_have_zero_half_word_at_bottom = () => _halfBottom.ShouldEqual((UInt16) 0);
        It should_have_zero_byte_at_bottom = () => _byteBottom.ShouldEqual((Byte) 0);

        It should_have_zero_word_at_middle = () => _wordMiddle.ShouldEqual(0u);
        It should_have_zero_half_word_at_middle = () => _halfMiddle.ShouldEqual((UInt16) 0);
        It should_have_zero_byte_at_middle = () => _byteMiddle.ShouldEqual((Byte) 0);

        It should_have_zero_word_at_top = () => _wordTop.ShouldEqual(0u);
        It should_have_zero_half_word_at_top = () => _halfTop.ShouldEqual((UInt16) 0);
        It should_have_zero_byte_at_top = () => _byteTop.ShouldEqual((Byte) 0);

        static UInt32 _wordBottom;
        static UInt16 _halfBottom;
        static Byte _byteBottom;
        static UInt32 _wordMiddle;
        static UInt16 _halfMiddle;
        static Byte _byteMiddle;
        static UInt32 _wordTop;
        static UInt16 _halfTop;
        static Byte _byteTop;
    }

    [Subject(typeof(Mmu))]
    public class When_reading_values_after_writing : WithSubject<Mmu>
    {
        Establish context = () =>
            {
                Subject.WriteWord(10, 0x12345678);
                Subject.WriteHalfWord(20, 0x1234);
                Subject.WriteByte(30, 0x12);
            };

        Because of = () =>
            {
                _word = Subject.ReadWord(10);
                _halfWord = Subject.ReadHalfWord(20);
                _byte = Subject.ReadByte(30);
            };

        It should_read_word_value = () => _word.ShouldEqual(0x12345678u);
        It should_read_half_word_value = () => _halfWord.ShouldEqual((UInt16) 0x1234);
        It should_read_byte_value = () => _byte.ShouldEqual((Byte) 0x12);

        static UInt32 _word;
        static UInt16 _halfWord;
        static Byte _byte;
    }

    [Subject(typeof(Mmu))]
    public class When_reading_same_address_in_different_bits : WithSubject<Mmu>
    {
        Establish context = () => Subject.WriteWord(address, 0xFEDBCA98);

        Because of = () =>
            {
                _halfword1 = Subject.ReadHalfWord(address);
                _halfword2 = Subject.ReadHalfWord(address+2);
                _byte1 = Subject.ReadByte(address);
                _byte2 = Subject.ReadByte(address+1);
                _byte3 = Subject.ReadByte(address+2);
                _byte4 = Subject.ReadByte(address+3);
            };

        It should_read_half_word_values = () =>
            {
                _halfword1.ShouldEqual((UInt16) 0xCA98);
                _halfword2.ShouldEqual((UInt16) 0xFEDB);
            };

        It should_read_byte_values = () =>
            {
                _byte1.ShouldEqual((Byte) 0x98);
                _byte2.ShouldEqual((Byte) 0xCA);
                _byte3.ShouldEqual((Byte) 0xDB);
                _byte4.ShouldEqual((Byte) 0xFE);
            };

        const uint address = 10;
        static UInt16 _halfword1, _halfword2;
        static Byte _byte1, _byte2, _byte3, _byte4;
    }

    [Subject(typeof(Mmu))]
    public class When_reading_exceeds_memory_space : WithSubject<Mmu>
    {
        Because of = () =>
            {
                _wordError = Catch.Exception(() => Subject.ReadWord(Subject.Size - 3));
                _halfError = Catch.Exception(() => Subject.ReadHalfWord(Subject.Size - 1));
                _byteError = Catch.Exception(() => Subject.ReadByte(Subject.Size));
            };

        It should_fail_at_word_size_boundary = () => _wordError.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_at_half_word_size_boundary = () => _halfError.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_at_byte_size_boundary = () => _byteError.ShouldBeOfType(typeof (BoundaryException));

        static Exception _wordError;
        static Exception _halfError;
        static Exception _byteError;
    }

    [Subject(typeof(Mmu))]
    public class When_writing_exceeds_memory_space : WithSubject<Mmu>
    {
        Because of = () =>
            {
                _wordError = Catch.Exception(() => Subject.WriteWord(Subject.Size - 3, 1));
                _halfError = Catch.Exception(() => Subject.WriteHalfWord(Subject.Size - 1, 1));
                _byteError = Catch.Exception(() => Subject.WriteByte(Subject.Size, 1));
            };

        It should_fail_at_word_size_boundary = () => _wordError.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_at_half_word_size_boundary = () => _halfError.ShouldBeOfType(typeof (BoundaryException));
        It should_fail_at_byte_size_boundary = () => _byteError.ShouldBeOfType(typeof (BoundaryException));

        static Exception _wordError;
        static Exception _halfError;
        static Exception _byteError;
    }
}
