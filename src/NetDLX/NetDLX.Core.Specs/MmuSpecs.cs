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
            };

        It should_have_zero_word_at_bottom = () => _wordBottom.ShouldEqual(0u);
        It should_have_zero_half_word_at_bottom = () => _halfBottom.ShouldEqual((UInt16) 0);
        It should_have_zero_byte_at_bottom = () => _byteBottom.ShouldEqual((Byte) 0);

        It should_have_zero_word_in_middle = () => _wordMiddle.ShouldEqual(0u);
        It should_have_zero_half_word_in_middle = () => _halfMiddle.ShouldEqual((UInt16) 0);
        It should_have_zero_byte_in_middle = () => _byteMiddle.ShouldEqual((Byte) 0);

        static UInt32 _wordBottom;
        static UInt16 _halfBottom;
        static Byte _byteBottom;
        static UInt32 _wordMiddle;
        static UInt16 _halfMiddle;
        static Byte _byteMiddle;
    }
}
