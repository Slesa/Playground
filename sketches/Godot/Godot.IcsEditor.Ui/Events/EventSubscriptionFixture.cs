using System;
using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;

namespace Godot.IcsEditor.Ui.Events
{
    [Subject(typeof(EventSubscription<>))]
    public class When_subscribe_with_null_action
    {
        Establish context = () =>
        {
            _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _filterDelegate.Stub(x => x.Target).Return((Predicate<object>)(arg => true));
        };

        Because of = () =>
        {
            _exception = Catch.Exception(() =>
            { _eventSubscription = new EventSubscription<object>(null, _filterDelegate); });
        };

        It should_fail = () => _exception.ShouldBeOfType(typeof(ArgumentNullException));

        static IDelegateReference _filterDelegate;
        static Exception _exception;
        static EventSubscription<object> _eventSubscription;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_subscribe_with_null_filter
    {
        Establish context = () =>
        {
            _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _actionDelegate.Stub(x => x.Target).Return((Action<object>)delegate { });
        };

        Because of = () =>
        {
            _exception = Catch.Exception(() =>
            { _eventSubscription = new EventSubscription<object>(_actionDelegate, null); });
        };

        It should_fail = () => _exception.ShouldBeOfType(typeof(ArgumentNullException));

        static IDelegateReference _actionDelegate;
        static Exception _exception;
        static EventSubscription<object> _eventSubscription;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_subscribe_with_null_action_target
    {
        Establish context = () =>
            {
                _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _actionDelegate.Stub(x => x.Target).Return(null);
                _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _filterDelegate.Stub(x => x.Target).Return((Predicate<object>)(arg => true));
            };

        Because of = () =>
            {
                _exception = Catch.Exception(() =>
                    { _eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate); });
            };

        It should_fail = () => _exception.ShouldBeOfType(typeof(ArgumentException));

        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static Exception _exception;
        static EventSubscription<object> _eventSubscription;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_subscribe_with_null_filter_target
    {
        Establish context = () =>
        {
            _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _actionDelegate.Stub(x => x.Target).Return((Action<object>)delegate { });
            _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _filterDelegate.Stub(x => x.Target).Return(null);
        };

        Because of = () =>
        {
            _exception = Catch.Exception(() =>
            { _eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate); });
        };

        It should_fail = () => _exception.ShouldBeOfType(typeof(ArgumentException));

        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static Exception _exception;
        static EventSubscription<object> _eventSubscription;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_subscribe_with_different_type_in_action
    {
        Establish context = () =>
        {
            _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _actionDelegate.Stub(x => x.Target).Return((Action<int>)delegate { });
            _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _filterDelegate.Stub(x => x.Target).Return((Predicate<object>)(arg => true));
        };

        Because of = () =>
        {
            _exception = Catch.Exception(() =>
            { _eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate); });
        };

        It should_fail = () => _exception.ShouldBeOfType(typeof(ArgumentException));

        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static Exception _exception;
        static EventSubscription<object> _eventSubscription;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_subscribe_with_different_type_in_filter
    {
        Establish context = () =>
        {
            _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _actionDelegate.Stub(x => x.Target).Return((Action<object>)delegate { });
            _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _filterDelegate.Stub(x => x.Target).Return((Predicate<int>)(arg => true));
        };

        Because of = () =>
        {
            _exception = Catch.Exception(() =>
            { _eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate); });
        };

        It should_fail = () => _exception.ShouldBeOfType(typeof(ArgumentException));

        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static Exception _exception;
        static EventSubscription<object> _eventSubscription;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_subscribing_with_valid_values
    {
        Establish context = () =>
        {
            _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _actionDelegate.Stub(x => x.Target).Return((Action<object>)delegate { });
            _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _filterDelegate.Stub(x => x.Target).Return((Predicate<object>)delegate { return true; });
            _subscriptionToken = new SubscriptionToken();
        };

        Because of = () =>
        {
            _eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate) { SubscriptionToken = _subscriptionToken };
        };

        It should_match_action = () => _eventSubscription.Action.ShouldEqual(_actionDelegate.Target);
        It should_match_filter = () => _eventSubscription.Filter.ShouldEqual(_filterDelegate.Target);
        It should_match_token = () => _eventSubscription.SubscriptionToken.ShouldEqual(_subscriptionToken);


        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static EventSubscription<object> _eventSubscription;
        static SubscriptionToken _subscriptionToken;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_publishing_event_with_subscription
    {
        Establish context = () =>
            {
                _executedDelegates = new List<string>();
                _mocks = new MockRepository();
                _actionDelegate = _mocks.DynamicMock<IDelegateReference>();
                _filterDelegate = _mocks.DynamicMock<IDelegateReference>();

                using (_mocks.Record())
                {
                    Expect.Call(_actionDelegate.Target).Return((Action<object>)delegate { _executedDelegates.Add("Action"); });
                    Expect.Call(_filterDelegate.Target).Return((Predicate<object>)delegate { _executedDelegates.Add("Filter"); return true; });
                }
            };

        Because of = () =>
        {
            using (_mocks.Playback())
            {
                var eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate);
                var publishAction = eventSubscription.GetExecutionStrategy();
                publishAction.Invoke(null);
            }
        };

        It should_match_expectations = () => _mocks.VerifyAll();
        It should_execute_action_and_filter = () => _executedDelegates.Count.ShouldEqual(2);
        It should_execute_filter_first = () => _executedDelegates[0].ShouldEqual("Filter");
        It should_execute_action_last = () => _executedDelegates[1].ShouldEqual("Action");

        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static MockRepository _mocks;
        static List<string> _executedDelegates;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_get_publish_action_with_action_set_to_null
    {
        Establish context = () =>
            {
                _actionDelegate = new MockDelegateReference { Target = (Action<object>)delegate { } };
                _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _filterDelegate.Stub(x => x.Target).Return((Predicate<object>)delegate { return true; });
            };

        Because of = () =>
        {
            var eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate);
            _publishAction = eventSubscription.GetExecutionStrategy();
            _publishAction.ShouldNotBeNull();
            _actionDelegate.Target = null;
            _publishAction = eventSubscription.GetExecutionStrategy();
        };

        It should_return_null = () => _publishAction.ShouldBeNull();

        static MockDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static Action<object[]> _publishAction;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_get_publish_action_with_filter_set_to_null
    {

        Establish context = () =>
        {
            _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
            _actionDelegate.Stub(x => x.Target).Return((Action<object>)delegate { });
            _filterDelegate = new MockDelegateReference { Target = (Predicate<object>)delegate { return true; } };
        };

        Because of = () =>
        {
            var eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate);
            _publishAction = eventSubscription.GetExecutionStrategy();
            _publishAction.ShouldNotBeNull();
            _filterDelegate.Target = null;
            _publishAction = eventSubscription.GetExecutionStrategy();
        };

        It should_return_null = () => _publishAction.ShouldBeNull();

        static IDelegateReference _actionDelegate;
        static MockDelegateReference _filterDelegate;
        static Action<object[]> _publishAction;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_get_publish_action_with_filter_is_false
    {

        Establish context = () =>
            {
                _actionExecuted = false;
                _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _actionDelegate.Stub(x => x.Target).Return((Action<object>)delegate { _actionExecuted = true; });
                _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _filterDelegate.Stub(x => x.Target).Return((Predicate<object>)delegate { return false; });
            };

        Because of = () =>
            {
                var eventSubscription = new EventSubscription<object>(_actionDelegate, _filterDelegate);
                var publishAction = eventSubscription.GetExecutionStrategy();
                publishAction.Invoke(new object[] {null});
            };

        It should_not_execute_action = () => _actionExecuted.ShouldBeFalse();

        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static bool _actionExecuted;
    }

    [Subject(typeof(EventSubscription<>))]
    public class When_passing_arguments_to_delegates
    {

        Establish context = () =>
            {
                _passedArgumentToAction = null;
                _passedArgumentToFilter = null;
                _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _actionDelegate.Stub(x => x.Target).Return((Action<string>) (obj => _passedArgumentToAction = obj));
                _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _filterDelegate.Stub(x => x.Target).Return(
                    (Predicate<string>) (obj =>
                        {
                            _passedArgumentToFilter = obj;
                            return true;
                        }));
            };

        Because of = () =>
        {
            var eventSubscription = new EventSubscription<string>(_actionDelegate, _filterDelegate);
            var publishAction = eventSubscription.GetExecutionStrategy();
            publishAction.Invoke(new [] { "TestString" });
        };

        It should_pass_argument_to_action = () => _passedArgumentToAction.ShouldEqual("TestString");
        It should_pass_argument_to_filter = () => _passedArgumentToFilter.ShouldEqual("TestString");

        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
        static string _passedArgumentToAction;
        static string _passedArgumentToFilter;
    }

    // IDelegateReference hat keinen Setter für Target
    class MockDelegateReference : IDelegateReference
    {
        public Delegate Target { get; set; }
    }


}