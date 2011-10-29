using System;
using Machine.Fakes;
using Machine.Specifications;
using NetDLX.Core.Exceptions;

namespace NetDLX.Code.Specs
{
    [Subject(typeof(FpuOpBuilder))]
    public class FpuOpBuilderSpecsBase : WithSubject<FpuOpBuilder>
    {
        Establish context = () => Program = new Program();

        protected static Program Program;
    }

    [Subject(typeof(FpuOpBuilder))]
    public class When_translating_outcommented_fpuop : FpuOpBuilderSpecsBase
    {
        Because of = () =>
            {
                _canHandle = Subject.CanHandle(Source);
                _result = Subject.Handle(Program, Source);
            };

        It should_not_handle_source = () => _canHandle.ShouldBeFalse();
        It should_not_touch_source = () => _result.ShouldEqual(Source);
        It should_not_add_code = () => Program.Code.Count.ShouldEqual(0);

        static bool _canHandle;
        static string _result;
        const string Source = "    ;   addf f3,f4,f5";
    }

    [Subject(typeof(FpuOpBuilder))]
    public class When_translating_no_fpuop : FpuOpBuilderSpecsBase
    {
        Because of = () =>
            {
                _canHandle = Subject.CanHandle(Source);
                _result = Subject.Handle(Program, Source);
            };

        It should_not_handle_source = () => _canHandle.ShouldBeFalse();
        It should_not_touch_source = () => _result.ShouldEqual(Source);
        It should_not_add_code = () => Program.Code.Count.ShouldEqual(0);

        static bool _canHandle;
        static string _result;
        const string Source = "    lhi r1,0X10";
    }

    [Subject(typeof(FpuOpBuilder))]
    public class When_translating_fpu_add_double : FpuOpBuilderSpecsBase
    {
        Because of = () =>
            {
                _canHandle = Subject.CanHandle(Source);
                _result = Subject.Handle(Program, Source);
            };

        It should_handle_source = () => _canHandle.ShouldBeTrue();
        It should_consume_source = () => _result.ShouldBeEmpty();
        It should_add_code = () => Program.Code.Count.ShouldEqual(1);
        It should_set_current_address = () => Program.CurrentAddress.ShouldEqual(1);
        It should_calc_opcode = () => Program.Code[0].ShouldEqual<uint>(0x8c221800);

        static bool _canHandle;
        static string _result;
        const string Source = "addd d1, d2, d3";
    }

    [Subject(typeof(FpuOpBuilder))]
    public class When_translating_fpu_add_with_too_less_params : FpuOpBuilderSpecsBase
    {
        Because of = () =>
            {
                _error = Catch.Exception( () => _result = Subject.Handle(Program, Source) );
            };

        It should_fail = () => _error.ShouldBeOfType(typeof (IndexOutOfRangeException));

        static string _result;
        static Exception _error;
        const string Source = "addd d1,d2";
    }

    [Subject(typeof(FpuOpBuilder))]
    public class When_translating_fpu_add_with_wrong_params : FpuOpBuilderSpecsBase
    {
        Because of = () =>
            {
                _error = Catch.Exception( () => _result = Subject.Handle(Program, Source) );
            };

        It should_fail = () => _error.ShouldBeOfType(typeof (SyntaxErrorException));

        static string _result;
        static Exception _error;
        const string Source = "addd r1,r2";
    }


}