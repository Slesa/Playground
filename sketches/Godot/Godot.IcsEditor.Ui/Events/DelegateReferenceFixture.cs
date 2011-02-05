using System;
using Machine.Specifications;

namespace Godot.IcsEditor.Ui.Events
{
    [Subject(typeof(DelegateReference))]
    public class When_garbage_collection_happens_to_a_kept_alive_delegate
    {
        Establish context = () =>
            {
                _delegates = new SomeClassHandler();
                _delegateReference = new DelegateReference((Action<string>)_delegates.DoEvent, true);
            };

        Because of = () =>
            {
                _delegates = null;
                GC.Collect();
            };

        It should_have_target_anyway = () => _delegateReference.Target.ShouldNotBeNull();

        static SomeClassHandler _delegates;
        static DelegateReference _delegateReference;
    }

    [Subject(typeof(DelegateReference))]
    public class When_garbage_collection_happens_to_a_nokeep_alive_delegate
    {
        Establish context = () =>
        {
            _delegates = new SomeClassHandler();
            _delegateReference = new DelegateReference((Action<string>)_delegates.DoEvent, false);
        };

        Because of = () =>
        {
            _delegates = null;
            GC.Collect();
        };

        It should_have_no_target_anymore = () => _delegateReference.Target.ShouldBeNull();

        static SomeClassHandler _delegates;
        static DelegateReference _delegateReference;
    }

    [Subject(typeof(DelegateReference))]
    public class When_garbage_collection_happens_to_a_still_existing_delegate
    {
        Establish context = () =>
        {
            _delegates = new SomeClassHandler();
            _delegateReference = new DelegateReference((Action<string>)_delegates.DoEvent, false);
        };

        Because of = () =>
        {
            GC.Collect();
            _stillLiving = _delegates != null;
            _delegates = null;
            GC.Collect();
            _nowDead = _delegates == null;
        };

        It should_live_when_still_used = () => _stillLiving.ShouldBeTrue();
        It should_be_collected_when_unused = () => _nowDead.ShouldBeTrue();

        static SomeClassHandler _delegates;
        static DelegateReference _delegateReference;
        static bool _stillLiving;
        static bool _nowDead;
    }

    [Subject(typeof(DelegateReference))]
    public class When_using_a_target
    {
        Establish context = () =>
            {
                _classHandler = new SomeClassHandler();
                _myAction = new Action<string>(_classHandler.MyAction);
                _weakAction = new DelegateReference(_myAction, false);

            };

        Because of = () => ((Action<string>)_weakAction.Target)("payload");

        It should_return_the_action = () => _classHandler.MyActionArg.ShouldEqual("payload");

        static SomeClassHandler _classHandler;
        static Action<string> _myAction;
        static DelegateReference _weakAction;
    }

    [Subject(typeof(DelegateReference))]
    public class When_collecting_original_delegate
    {
        Establish context = () =>
            {
                _classHandler = new SomeClassHandler();
                _myAction = new Action<string>(_classHandler.MyAction);
                _weakAction = new DelegateReference(_myAction, false);
                _originalAction = new WeakReference(_myAction);
            };

        Because of = () =>
            {
                _myAction = null;
                GC.Collect();
                _isAlive = _originalAction.IsAlive;
                ((Action<string>)_weakAction.Target)("payload");
            };

        It should_return_the_action = () =>
            {
                _isAlive.ShouldBeFalse();
                _classHandler.MyActionArg.ShouldEqual("payload");
            };

        static SomeClassHandler _classHandler;
        static Action<string> _myAction;
        static DelegateReference _weakAction;
        static WeakReference _originalAction;
        static bool _isAlive;
    }

    [Subject(typeof(DelegateReference))]
    public class When_target_is_not_alive
    {
        Establish context = () =>
            {
                _classHandler = new SomeClassHandler();
                _weakHandler = new WeakReference(_classHandler);
                _action = new DelegateReference((Action<string>)_classHandler.DoEvent, false);
            };

        Because of = () =>
            {
                _classHandler = null;
                GC.Collect();
            };

        It should_return_null = () =>
            {
                _weakHandler.IsAlive.ShouldBeFalse();
                _action.Target.ShouldBeNull();
            };

        static SomeClassHandler _classHandler;
        static WeakReference _weakHandler;
        static DelegateReference _action;
    }

    [Subject(typeof(DelegateReference))]
    public class When_using_static_methods
    {
        Establish context = () =>
        {
            _action = new DelegateReference((Action)SomeClassHandler.StaticMethod, false);
        };

        It should_work = () => _action.Target.ShouldNotBeNull();

        static DelegateReference _action;
    }

    [Subject(typeof(DelegateReference))]
    public class When_using_null_delegate
    {
        Because of = () =>
            {
                _exception = Catch.Exception(() => { _action = new DelegateReference(null, true); });
            };

        It should_fail = () => _exception.ShouldBeOfType(typeof(ArgumentNullException));
        static Exception _exception;
        static DelegateReference _action;
    }


#pragma warning disable 168
    internal class SomeClassHandler
    {
        public string MyActionArg;

        public void DoEvent(string value)
        {
            var myValue = value;
        }

        public static void StaticMethod()
        {
            var i = 0;
        }

        public void MyAction(string arg)
        {
            MyActionArg = arg;
        }
    }
#pragma warning restore 168

}