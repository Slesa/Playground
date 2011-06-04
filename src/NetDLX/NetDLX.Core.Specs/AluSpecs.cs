using System;
using Machine.Fakes;
using Machine.Specifications;

namespace NetDLX.Core.Specs
{
    [Subject(typeof(Alu))]
    public class When_adding_two_numbers : WithSubject<Alu>
    {
        Because of = () => _sum = Subject.Add(0xC0C0u, 0x0D0Du);

        It should_be_correct = () => _sum.ShouldEqual(0xCDCDu);

        static UInt32 _sum;
    }

    [Subject(typeof(Alu))]
    public class When_subtracting_two_numbers : WithSubject<Alu>
    {
        Because of = () => _difference = Subject.Sub(0xCDCDu, 0xC0C0u);

        It should_be_correct = () => _difference.ShouldEqual(0x0D0Du);

        static UInt32 _difference;
    }

    [Subject(typeof(Alu))]
    public class When_multiplying_two_floats : WithSubject<Alu>
    {
        Because of = () => _product = Subject.Mult(0.42f, 0.42f);

        It should_be_correct = () => _product.ShouldEqual(0.42f*0.42f); // 0.1764f);

        static float _product;
    }

    [Subject(typeof(Alu))]
    public class When_dividing_two_floats : WithSubject<Alu>
    {
        Because of = () => _division = Subject.Div(0.1764f, 0.42f);

        It should_be_correct = () => _division.ShouldEqual(0.1764f/0.42f); //0.42f);

        static float _division;
    }

    [Subject(typeof(Alu))]
    public class When_multiplying_two_doubles : WithSubject<Alu>
    {
        Because of = () => _product = Subject.Mult(0.42, 0.42);

        It should_be_correct = () => _product.ShouldEqual(0.42*0.42); //0.1764);

        static double _product;
    }

    [Subject(typeof(Alu))]
    public class When_dividing_two_doubles : WithSubject<Alu>
    {
        Because of = () => _division = Subject.Div(0.1764, 0.42);

        It should_be_correct = () => _division.ShouldEqual(0.1764/0.42); //0.42);

        static double _division;
    }

    [Subject(typeof(Alu))]
    public class When_performing_and : WithSubject<Alu>
    {
        Because of = () => _and = Subject.And(0x71C7u, 0x2AAAu);

        It should_be_correct = () => _and.ShouldEqual(0x2082u);

        static UInt32 _and;
    }

    [Subject(typeof(Alu))]
    public class When_performing_or : WithSubject<Alu>
    {
        Because of = () => _or = Subject.Or(0x71C7u, 0x2AAAu);

        It should_be_correct = () => _or.ShouldEqual(0x7BEFu);

        static UInt32 _or;
    }

    [Subject(typeof(Alu))]
    public class When_performing_xor : WithSubject<Alu>
    {
        Because of = () => _xor = Subject.Xor(0x71C7u, 0x2AAAu);

        It should_be_correct = () => _xor.ShouldEqual(0x5B6Du);

        static UInt32 _xor;
    }
}