using System;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Events
{
    [Subject(typeof(DataEventArgs<>))]
    public class When_using_data_event_args
    {
        Establish context = () => { _eventArgs = new DataEventArgs<int>(42); };

        It Should_pass_data = () => _eventArgs.Value.ShouldEqual(42);
        It should_be_an_event_arg = () => typeof (EventArgs).IsAssignableFrom(typeof (DataEventArgs<>)).ShouldBeTrue();

        static DataEventArgs<int> _eventArgs;
    }
}