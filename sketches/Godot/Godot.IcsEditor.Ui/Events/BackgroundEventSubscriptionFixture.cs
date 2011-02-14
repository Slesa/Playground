using System;
using System.Threading;
using Machine.Specifications;
using Rhino.Mocks;

namespace Godot.IcsEditor.Ui.Events
{
    [Subject(typeof(BackgroundEventSubscription<>))]
    public class When_working_with_different_threads
    {
        Establish context = () =>
            {
                _calledThreadId = 1;
                _completeEvent = new ManualResetEvent(false);

                _actionDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _actionDelegate.Stub(x => x.Target).Return(
                    (Action<object>) delegate
                        {
                            _calledThreadId = Thread.CurrentThread.ManagedThreadId;
                            _completeEvent.Set();
                        });
                _filterDelegate = MockRepository.GenerateStub<IDelegateReference>();
                _filterDelegate.Stub(x => x.Target).Return((Predicate<object>)delegate { return true; });
            };

        Because of = () =>
            {
                var eventSubscription = new BackgroundEventSubscription<object>(_actionDelegate, _filterDelegate);
                var publishAction = eventSubscription.GetExecutionStrategy();
                publishAction.Invoke(null);

#if SILVERLIGHT
                _completeEvent.WaitOne(5000);
#else
                _completeEvent.WaitOne(5000, false);
#endif
            };

        It should_work_in_different_threads = () => _calledThreadId.ShouldNotEqual(Thread.CurrentThread.ManagedThreadId);

        static int _calledThreadId;
        static ManualResetEvent _completeEvent;
        static IDelegateReference _actionDelegate;
        static IDelegateReference _filterDelegate;
    }
}