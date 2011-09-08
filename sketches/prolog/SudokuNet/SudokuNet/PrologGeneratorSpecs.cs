using System;
using Machine.Fakes;
using Machine.Specifications;

namespace SudokuNet
{
    [Subject(typeof(PrologGenerator))]
    public class When_constructing_validation_part : WithSubject<PrologGenerator>
    {
        Establish context = () =>
            {
                Validation = 
                "is_valid([])." + Environment.NewLine +
                "is_valid([Head|Tail]) :-" + Environment.NewLine +
                "  fd_all_different(Head)," + Environment.NewLine +
                "  is_valid(Tail)." + Environment.NewLine
                + Environment.NewLine;

            };
        Because of = () => _validation = Subject.Validation;

        It should_return_the_right_sequence = () =>
            {
                _validation.ShouldEqual(Validation);
            };

        static string _validation;

        static string Validation;
    }
}