using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Events
{
    [Subject(typeof(EventAggregator))]
    public class When_using_same_event_type
    {
        Establish context = () => { _eventAggregator = new EventAggregator(); };

        Because of = () =>
            {
                _instance1 = _eventAggregator.GetEvent<MockEventBase>();
                _instance2 = _eventAggregator.GetEvent<MockEventBase>();
            };

        It should_be_the_same_instance = () => _instance1.ShouldBeTheSameAs(_instance2);

        static EventAggregator _eventAggregator;
        static EventBase _instance1;
        static EventBase _instance2;
    }

    class MockEventBase : EventBase { }
}