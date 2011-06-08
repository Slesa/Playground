using System;
using System.Collections.Generic;
using Machine.Fakes;
using Machine.Specifications;
using NetDLX.Core.Exceptions;

namespace NetDLX.Core.Specs
{
    [Subject(typeof(AssembleBase))]
    public class AssembleBaseTest
    {
        IEnumerable<CpuOperation> KnownCommands = new List<CpuOperation>
            {
                new CpuOperation {Mnemonic = "LB", OpCode = 0x10, Operands = "RR"},
                new CpuOperation {Mnemonic = "LBU", OpCode = 0x11, Operands = "RR"},
                new CpuOperation {Mnemonic = "LH", OpCode = 0x12, Operands = "RR"},
            };

        public bool CanAssemble(string command)
        {
            return AssembleBase.CanAssemble(command, KnownCommands);
        }

        public UInt32 Assemble(string command, string[] args)
        {
            return AssembleBase.Assemble(command, args, KnownCommands);
        }
    }

    [Subject(typeof(AssembleBase))]
    public class When_asking_for_unknown_data_command : WithSubject<AssembleBaseTest>
    {
        Because of = () => _canAssemble = Subject.CanAssemble("add");
        It should_reject = () => _canAssemble.ShouldBeFalse();
        static bool _canAssemble;
    }

    [Subject(typeof(AssembleBase))]
    public class When_asking_for_known_data_command : WithSubject<AssembleBaseTest>
    {
        Because of = () => _canAssemble = Subject.CanAssemble("LB");
        It should_accept = () => _canAssemble.ShouldBeTrue();
        static bool _canAssemble;
    }

    [Subject(typeof(AssembleBase))]
    public class When_asking_for_known_data_command_case_insensitive : WithSubject<AssembleBaseTest>
    {
        Because of = () => _canAssemble = Subject.CanAssemble("Lb");
        It should_accept = () => _canAssemble.ShouldBeTrue();
        static bool _canAssemble;
    }

    [Subject(typeof(AssembleBase))]
    public class When_assembling_unknown_data_command : WithSubject<AssembleBaseTest>
    {
        Because of = () => _error = Catch.Exception(() => Subject.Assemble("add", null));
        It should_fail = () => _error.ShouldBeOfType(typeof(UnknownCommandException));
        static Exception _error;
    }

    [Subject(typeof(AssembleBase))]
    public class When_assembling_command_without_arguments : WithSubject<AssembleBaseTest>
    {
        Because of = () => _error = Catch.Exception(() => Subject.Assemble("lb", null));
        It should_fail = () => _error.ShouldBeOfType(typeof(SyntaxErrorException));
        static Exception _error;
    }

    [Subject(typeof(AssembleBase))]
    public class When_assembling_command_with_not_enough_arguments : WithSubject<AssembleBaseTest>
    {
        Because of = () => _error = Catch.Exception(() => Subject.Assemble("lb", new[] { "r1" }));
        It should_fail = () => _error.ShouldBeOfType(typeof(SyntaxErrorException));
        static Exception _error;
    }

    [Subject(typeof(AssembleBase))]
    public class When_assembling_command_with_wrong_arguments : WithSubject<AssembleBaseTest>
    {
        Because of = () => _error = Catch.Exception(() => Subject.Assemble("lb", new[] { "f1", "f2" }));
        It should_fail = () => _error.ShouldBeOfType(typeof(SyntaxErrorException));
        static Exception _error;
    }

    [Subject(typeof(AssembleBase))]
    public class When_assembling_different_commands : WithSubject<AssembleBaseTest>
    {
        Because of = () =>
            {
                _op1 = Subject.Assemble("lb", new[] { "r1", "r2", "r3" });
                _op2 = Subject.Assemble("lh", new[] { "r1", "r2", "r3" });
            };

        It opcodes_should_differ = () => _op1.ShouldNotEqual(_op2);

        static UInt32 _op1;
        static UInt32 _op2;
    }
}
