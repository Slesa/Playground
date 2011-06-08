using Machine.Fakes;
using Machine.Specifications;

namespace NetDLX.Core.Specs
{
    [Subject(typeof(Assembler))]
    public class When_assembling_empty_script : WithSubject<Assembler>
    {
        It should_run = () => Subject.run();
    }

    [Subject(typeof(Assembler))]
    public class When_assembling_simple_script : WithSubject<Assembler>
    {
        static string[] simpleScript = new [] {
            "lw r2, 42 ",
            "add r2, 1",
            "sw " };

        Establish context = () => Subject.Source = simpleScript;
        It should_create_binary = () => Subject.Binary.ShouldNotBeEmpty();
    }
}