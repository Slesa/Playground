using Machine.Fakes;
using Machine.Specifications;

namespace NetDLX.Code.Specs
{
    [Subject(typeof(ProgramBuilder))]
    public class When_translating_outcommented_label : WithSubject<ProgramBuilder>
    {
        Because of = () => _program = Subject.Generate("    ;   start:");

        It should_not_add_label_entry = () => _program.Labels.Count.ShouldEqual(0);

        static Program _program;
    }

    [Subject(typeof(ProgramBuilder))]
    public class When_translating_no_label : WithSubject<ProgramBuilder>
    {
        Because of = () => _program = Subject.Generate("    lhi r1,0X10");

        It should_not_add_label_entry = () => _program.Labels.Count.ShouldEqual(0);

        static Program _program;
    }

    [Subject(typeof(ProgramBuilder))]
    public class When_translating_jump_label : WithSubject<ProgramBuilder>
    {
        Because of = () => _program = Subject.Generate("start:");

        It should_add_label_entry = () => _program.Labels.Count.ShouldEqual(1);
        It should_set_label_name = () => _program.Labels[0].Name.ShouldEqual("start");
        It should_set_label_type = () => _program.Labels[0].Type.ShouldEqual(LabelType.JUMP);
        It should_have_no_size = () => _program.Labels[0].Size.ShouldEqual(0);

        static Program _program;
    }

    [Subject(typeof(ProgramBuilder))]
    public class When_translating_jump_label_with_spaces : WithSubject<ProgramBuilder>
    {
        Because of = () => _program = Subject.Generate("start: lhi r1,0X10");

        It should_add_label_entry = () => _program.Labels.Count.ShouldEqual(1);
        It should_have_no_size = () => _program.Labels[0].Size.ShouldEqual(0);

        static Program _program;
    }

    [Subject(typeof(ProgramBuilder))]
    public class When_translating_jump_label_with_tabs : WithSubject<ProgramBuilder>
    {
        Because of = () => _program = Subject.Generate("start:	lhi	r1,0X10");

        It should_add_label_entry = () => _program.Labels.Count.ShouldEqual(1);
        It should_have_no_size = () => _program.Labels[0].Size.ShouldEqual(0);

        static Program _program;
    }

    [Subject(typeof(ProgramBuilder))]
    public class When_translating_data_label : WithSubject<ProgramBuilder>
    {
        Because of = () => _program = Subject.Generate("A:  .word	0xffffffff");

        It should_add_data_entry = () => _program.Labels.Count.ShouldEqual(1);
        It should_set_data_name = () => _program.Labels[0].Name.ShouldEqual("A");
        It should_set_data_type = () => _program.Labels[0].Type.ShouldEqual(LabelType.WORD);
        It should_have_correct_size = () => _program.Labels[0].Size.ShouldEqual(4);
        It should_have_set_value = () => _program.Labels[0].Value.ShouldEqual("0xffffffff");

        static Program _program;
    }

}