using Machine.Specifications;
using Rhino.Mocks;

namespace Godot.IcsEditor.Ui.Events
{
    [Subject(typeof(EventBase))]
    public class When_subscribing_events
    {
        Establish context = () =>
        {
            _eventBase = new TestableEventBase();

            _mocks = new MockRepository();
            _eventSubscription = _mocks.DynamicMock<IEventSubscription>();
            Expect.Call(_eventSubscription.SubscriptionToken).PropertyBehavior();
        };

        Because of = () => _eventBase.Subscribe(_eventSubscription);

        It should_have_token = () => _eventSubscription.SubscriptionToken.ShouldNotBeNull();
        It should_be_registered = () => _eventBase.Contains(_eventSubscription.SubscriptionToken).ShouldBeTrue();

        static MockRepository _mocks;
        static TestableEventBase _eventBase;
        static IEventSubscription _eventSubscription;

    }

    [Subject(typeof(EventBase))]
    public class When_unsubscribing_subscribed_event
    {
        Establish context = () =>
        {
            _eventBase = new TestableEventBase();
            _eventSubscription = MockRepository.GenerateStub<IEventSubscription>();
        };

        Because of = () =>
            {
                _token = _eventBase.Subscribe(_eventSubscription);
                _eventBase.Contains(_token).ShouldBeTrue();
                _eventBase.Unsubscribe(_token);
            };

        It should_be_unregistered = () => _eventBase.Contains(_token).ShouldBeFalse();
        
        static TestableEventBase _eventBase;
        static IEventSubscription _eventSubscription;
        static SubscriptionToken _token;
    }

    [Subject(typeof(EventBase))]
    public class When_subscribing_simple_events
    {
        Establish context = () =>
            {
                _eventBase = new TestableEventBase();
                _eventPublished = false;

                _mocks = new MockRepository();
                _eventSubscription = _mocks.DynamicMock<IEventSubscription>();

                using (_mocks.Record())
                {
                    Expect.Call(_eventSubscription.GetExecutionStrategy()).Return(delegate { _eventPublished = true; });
                }
            };

        Because of = () =>
        {
            _mocks.ReplayAll();
            _eventBase.Subscribe(_eventSubscription);
            _eventBase.Publish();
        };

        It should_have_been_published = () =>
            {
                _mocks.VerifyAll();
                _eventPublished.ShouldBeTrue();
            };

        static MockRepository _mocks;
        static TestableEventBase _eventBase;
        static IEventSubscription _eventSubscription;
        static bool _eventPublished;
    }

    [Subject(typeof(EventBase))]
    public class When_using_multiple_subscribers
    {
        Establish context = () =>
            {
                _eventBase = new TestableEventBase();
                _payload = new Payload();

                _received1 = null;
                _received2 = null;

                _mocks = new MockRepository();
                _eventSubscription1 = _mocks.DynamicMock<IEventSubscription>();
                _eventSubscription2 = _mocks.DynamicMock<IEventSubscription>();

                using (_mocks.Record())
                {
                    Expect.Call(_eventSubscription1.GetExecutionStrategy())
                        .Return( delegate(object[] args) { _received1 = args; });
                    Expect.Call(_eventSubscription2.GetExecutionStrategy())
                        .Return( delegate(object[] args) { _received2 = args; });
                }
            };

        Because of = () =>
            {
                using (_mocks.Playback())
                {
                    _eventBase.Subscribe(_eventSubscription1);
                    _eventBase.Subscribe(_eventSubscription2);
                    _eventBase.Publish(_payload);
                }
            };

        It should_have_been_published = () => _mocks.VerifyAll();
        It should_have_been_published_to_first = () => _received1[0].ShouldEqual(_payload);
        It should_have_been_published_to_second = () => _received2[0].ShouldEqual(_payload);

        static TestableEventBase _eventBase;
        static MockRepository _mocks;
        static IEventSubscription _eventSubscription1;
        static IEventSubscription _eventSubscription2;
        static Payload _payload;
        static object[] _received1;
        static object[] _received2;
    }

    [Subject(typeof(EventBase))]
    public class When_subscribing_with_null_action
    {
        Establish context = () =>
        {
            _eventBase = new TestableEventBase();

            _eventSubscription = MockRepository.GenerateStub<IEventSubscription>();
            _eventSubscription.Stub(x => x.GetExecutionStrategy()).Return(null);
        };

        Because of = () =>
        {
            _token = _eventBase.Subscribe(_eventSubscription);
            _eventBase.Contains(_token).ShouldBeTrue();
            _eventBase.Publish();
        };

        It should_have_been_purged = () => _eventBase.Contains(_token).ShouldBeFalse();

        static TestableEventBase _eventBase;
        static IEventSubscription _eventSubscription;
        static SubscriptionToken _token;
    }

    class TestableEventBase : EventBase
    {
        public SubscriptionToken Subscribe(IEventSubscription subscription)
        {
            return InternalSubscribe(subscription);
        }

        public void Publish(params object[] arguments)
        {
            InternalPublish(arguments);
        }
    }

    class Payload { }
}