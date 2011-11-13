using Machine.Fakes;
using Machine.Specifications;

namespace NetDLX.Code.Specs
{
    [Subject(typeof(LabelBuilder))]
    public class LabelBuilderSpecsBase : WithSubject<LabelBuilder>
    {
        Establish context = () => Program = new Program();

        protected static Program Program;
    }

    [Subject(typeof(LabelBuilder))]
    public class When_translating_outcommented_label : LabelBuilderSpecsBase
    {
        Because of = () =>
            {
                _canHandle = Subject.CanHandle(Source);
                _result = Subject.Handle(Program, Source);
            };

        It should_not_handle_source = () => _canHandle.ShouldBeFalse();
        It should_not_touch_source = () => _result.ShouldEqual(Source);
        It should_not_add_label_entry = () => Program.Labels.Count.ShouldEqual(0);
        
        static bool _canHandle;
        static string _result;
        const string Source = "    ;   start:";
    }

    [Subject(typeof(LabelBuilder))]
    public class When_translating_no_label : LabelBuilderSpecsBase
    {
        Because of = () =>
            {
                _canHandle = Subject.CanHandle(Source);
                _result = Subject.Handle(Program, Source);
            };

        It should_not_handle_source = () => _canHandle.ShouldBeFalse();
        It should_not_touch_source = () => _result.ShouldEqual(Source);
        It should_not_add_label_entry = () => Program.Labels.Count.ShouldEqual(0);

        static bool _canHandle;
        static string _result;
        const string Source = "    lhi r1,0X10";
    }

    [Subject(typeof(LabelBuilder))]
    public class When_translating_jump_label : LabelBuilderSpecsBase
    {
        Because of = () =>
            {
                _canHandle = Subject.CanHandle(Source);
                _result = Subject.Handle(Program, Source);
            };

        It should_handle_source = () => _canHandle.ShouldBeTrue();
        It should_consume_source = () => _result.ShouldBeEmpty();
        It should_add_label_entry = () => Program.Labels.Count.ShouldEqual(1);
        It should_set_label_name = () => Program.Labels[0].Name.ShouldEqual("start");
        It should_set_label_type = () => Program.Labels[0].Type.ShouldEqual(LabelType.JUMP);
        It should_set_label_size = () => Program.Labels[0].Size.ShouldEqual(0);
        It should_set_label_address = () => Program.Labels[0].Address.ShouldEqual(0);

        static bool _canHandle;
        static string _result;
        const string Source = "start:";
    }

    [Subject(typeof(LabelBuilder))]
    public class When_translating_jump_label_with_spaces : LabelBuilderSpecsBase
    {
        Because of = () =>
            {
                _canHandle = Subject.CanHandle(Source);
                _result = Subject.Handle(Program, Source);
            };

        It should_handle_source = () => _canHandle.ShouldBeTrue();
        It should_consume_source_parts = () => _result.ShouldEqual(Source.Substring(7));
        It should_add_label_entry = () => Program.Labels.Count.ShouldEqual(1);
        It should_set_label_size = () => Program.Labels[0].Size.ShouldEqual(0);

        static bool _canHandle;
        static string _result;
        const string Source = "start: lhi r1,0X10";
    }

    [Subject(typeof(LabelBuilder))]
    public class When_translating_jump_label_with_tabs : LabelBuilderSpecsBase
    {
        Because of = () =>
            {
                _canHandle = Subject.CanHandle(Source);
                _result = Subject.Handle(Program, Source);
            };

        It should_handle_source = () => _canHandle.ShouldBeTrue();
        It should_consume_source_parts = () => _result.ShouldEqual(BuilderHandlerBase.NormalizeSpaces(Source.Substring(7)));
        It should_add_label_entry = () => Program.Labels.Count.ShouldEqual(1);
        It should_set_label_size = () => Program.Labels[0].Size.ShouldEqual(0);

        static bool _canHandle;
        static string _result;
        const string Source = "start:	lhi	r1,0X10";
    }

    [Subject(typeof(LabelBuilder))]
    public class When_translating_word_label : LabelBuilderSpecsBase
    {
        Because of = () =>
        {
            _canHandle = Subject.CanHandle(Source);
            _result = Subject.Handle(Program, Source);
        };

        It should_handle_source = () => _canHandle.ShouldBeTrue();
        It should_consume_source = () => _result.ShouldBeEmpty();
        It should_add_data_entry = () => Program.Labels.Count.ShouldEqual(1);
        It should_set_data_name = () => Program.Labels[0].Name.ShouldEqual("A");
        It should_set_data_type = () => Program.Labels[0].Type.ShouldEqual(LabelType.WORD);
        It should_set_correct_size = () => Program.Labels[0].Size.ShouldEqual(4);
        It should_set_value = () => Program.Labels[0].Value.ShouldEqual("0xffffffff");
        It should_return_correct_value = () => Program.Labels[0].GetWord().ShouldEqual(0xffffffff);
        It should_set_current_address = () => Program.CurrentAddress.ShouldEqual(4);

        static bool _canHandle;
        static string _result;
        const string Source = "A:  .word	0xffffffff";
    }

    [Subject(typeof(LabelBuilder))]
    public class When_translating_two_word_labels : LabelBuilderSpecsBase
    {
        Because of = () =>
        {
            Subject.Handle(Program, SourceA);
            Subject.Handle(Program, SourceB);
        };

        It should_add_data_entriee = () => Program.Labels.Count.ShouldEqual(2);
        It should_set_first_data_name = () => Program.Labels[0].Name.ShouldEqual("A");
        It should_set_first_address = () => Program.Labels[0].Address.ShouldEqual(0);
        It should_return_correct_first_value = () => Program.Labels[0].GetWord().ShouldEqual(0xffffffff);
        It should_set_second_data_name = () => Program.Labels[1].Name.ShouldEqual("B");
        It should_set_second_address = () => Program.Labels[1].Address.ShouldEqual(4);
        It should_return_correct_second_value = () => Program.Labels[1].GetWord().ShouldEqual<uint>(0x12345678);
        It should_set_current_address = () => Program.CurrentAddress.ShouldEqual(8);

        static bool _canHandle;
        static string _result;
        const string SourceA = "A:  .word	0xffffffff";
        const string SourceB = "B:  .word	0x12345678";
    }


}