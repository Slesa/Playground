using Machine.Fakes;
using Machine.Specifications;

namespace NetDLX.Code.Specs
{
    [Subject(typeof(ProgramBuilder))]
    public class When_translating_empty_source : WithSubject<ProgramBuilder>
    {
        Because of = () => _program = Subject.Generate("    ");

        It should_generate_program = () => _program.ShouldNotBeNull();
        It should_have_space_for_labels = () => _program.Labels.ShouldNotBeNull();
        It should_add_no_labels = () => _program.Labels.Count.ShouldEqual(0);
        It should_have_space_for_code = () => _program.Code.ShouldNotBeNull();
        It should_add_no_code = () => _program.Code.Count.ShouldEqual(0);
        It should_set_current_address = () => _program.CurrentAddress.ShouldEqual(0);

        static Program _program;
    }

}
