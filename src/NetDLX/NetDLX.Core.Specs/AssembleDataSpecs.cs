using System;
using Machine.Fakes;
using Machine.Specifications;
using NetDLX.Core.Exceptions;

namespace NetDLX.Core.Specs
{
    [Subject(typeof(AssembleData))]
    public class When_asking_for_unknown_data_command : WithSubject<AssembleData>
    {
        Because of = () => _canAssemble = Subject.CanAssemble("add");
        It should_reject = () => _canAssemble.ShouldBeFalse();
        static bool _canAssemble;
    }

    [Subject(typeof(AssembleData))]
    public class When_asking_for_known_data_command : WithSubject<AssembleData>
    {
        Because of = () => _canAssemble = Subject.CanAssemble("LB");
        It should_accept = () => _canAssemble.ShouldBeTrue();
        static bool _canAssemble;
    }

    [Subject(typeof(AssembleData))]
    public class When_asking_for_known_data_command_case_insensitive : WithSubject<AssembleData>
    {
        Because of = () => _canAssemble = Subject.CanAssemble("Lb");
        It should_accept = () => _canAssemble.ShouldBeTrue();
        static bool _canAssemble;
    }

    [Subject(typeof(AssembleData))]
    public class When_assembling_unknown_data_command : WithSubject<AssembleData>
    {
        Because of = () => _error = Catch.Exception(()=> Subject.Translate("add", null));
        It should_fail = () => _error.ShouldBeOfType(typeof (UnknownCommandException));
        static Exception _error;
    }

    [Subject(typeof(AssembleData))]
    public class When_assembling_command_without_arguments : WithSubject<AssembleData>
    {
        Because of = () => _error = Catch.Exception(()=> Subject.Translate("lb", null));
        It should_fail = () => _error.ShouldBeOfType(typeof (SyntaxErrorException));
        static Exception _error;
    }

    [Subject(typeof(AssembleData))]
    public class When_assembling_command_with_not_enough_arguments : WithSubject<AssembleData>
    {
        Because of = () => _error = Catch.Exception(() => Subject.Translate("lb", new[] {"r1"}));
        It should_fail = () => _error.ShouldBeOfType(typeof (SyntaxErrorException));
        static Exception _error;
    }

    [Subject(typeof(AssembleData))]
    public class When_assembling_different_commands : WithSubject<AssembleData>
    {
        Because of = () =>
            {
                _op1 = Subject.Translate("lb", new[] { "r1", "r2", "r3"});
                _op2 = Subject.Translate("lw", new[] { "r1", "r2", "r3"});
            };

        It opcodes_should_differ = () => _op1.ShouldNotEqual(_op2);

        static UInt32 _op1;
        static UInt32 _op2;
    }
}
