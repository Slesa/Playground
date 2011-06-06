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
        Because of = () => _error = Catch.Exception(()=> Subject.Translate("lb", null));
        It should_fail = () => _error.ShouldBeOfType(typeof (UnknownCommandException));
        static Exception _error;
    }
}
