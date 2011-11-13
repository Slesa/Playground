using Machine.Specifications;
using NetDLX.Core.Exceptions;

namespace NetDLX.Code.Specs
{
    [Subject(typeof(TranslateOperands))]
    public class When_translate_unknown_operand
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('k', "r1"));
        It should_fail = () => _error.ShouldBeOfType(typeof (InvalidRegisterException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_with_wrong_gp_operand
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('R', "d1"));
        It should_fail = () => _error.ShouldBeOfType(typeof (SyntaxErrorException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_with_invalid_gp_register
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('R', "r32"));
        It should_fail = () => _error.ShouldBeOfType(typeof (InvalidRegisterException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_gp_register
    {
        Because of = () => _reg = TranslateOperands.Translate('R', "R29");
        It should_work = () => _reg.ShouldEqual<uint>(29);
        static uint _reg;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_with_wrong_fp_operand
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('F', "r1"));
        It should_fail = () => _error.ShouldBeOfType(typeof (SyntaxErrorException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_with_invalid_fp_register
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('F', "f32"));
        It should_fail = () => _error.ShouldBeOfType(typeof (InvalidRegisterException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_fp_register
    {
        Because of = () => _reg = TranslateOperands.Translate('F', "f29");
        It should_work = () => _reg.ShouldEqual<uint>(29);
        static uint _reg;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_with_wrong_dp_operand
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('D', "r1"));
        It should_fail = () => _error.ShouldBeOfType(typeof (SyntaxErrorException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_with_odd_dp_operand
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('D', "f3"));
        It should_fail = () => _error.ShouldBeOfType(typeof (InvalidRegisterException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_with_invalid_dp_register
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('D', "f32"));
        It should_fail = () => _error.ShouldBeOfType(typeof (InvalidRegisterException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_dp_register
    {
        Because of = () => _reg = TranslateOperands.Translate('D', "f28");
        It should_work = () => _reg.ShouldEqual<uint>(28);
        static uint _reg;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_invalid_immediate
    {
        Because of = () => _error = Catch.Exception(() => TranslateOperands.Translate('I', "r1"));
        It should_fail = () => _error.ShouldBeOfType(typeof(SyntaxErrorException));
        static object _error;
    }

    [Subject(typeof(TranslateOperands))]
    public class When_translate_immediate
    {
        Because of = () =>
            {
                _uint = TranslateOperands.Translate('I', "87654321");
                _hex = TranslateOperands.Translate('I',"0xFEDCBA98");
            };

        It should_translate_uint = () => _uint.ShouldEqual<uint>(87654321);
        It should_translate_hex = () => _hex.ShouldEqual(0xFEDCBA98);
        static uint _uint;
        static uint _hex;
    }
}
