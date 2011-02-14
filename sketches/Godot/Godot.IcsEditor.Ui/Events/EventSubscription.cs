using System;
using System.Globalization;

namespace Godot.IcsEditor.Ui.Events
{
    public class EventSubscription<TPayload> : IEventSubscription
    {
        readonly IDelegateReference _actionReference;
        readonly IDelegateReference _filterReference;

        public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            if( actionReference==null)
                throw new ArgumentNullException("actionReference");
            if( !(actionReference.Target is Action<TPayload>))
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "The Target of the IDelegateReference should be of type {0}.", typeof(Action<TPayload>).FullName), "actionReference");

            if( filterReference==null)
                throw new ArgumentNullException("filterReference");
            if( !(filterReference.Target is Predicate<TPayload>))
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "The Target of the IDelegateReference should be of type {0}.", typeof(Predicate<TPayload>).FullName), "filterReference");

            _actionReference = actionReference;
            _filterReference = filterReference;
        }

        public Action<TPayload> Action
        {
            get { return (Action<TPayload>) _actionReference.Target; }
        }

        public Predicate<TPayload> Filter
        {
            get { return (Predicate<TPayload>) _filterReference.Target; }
        }

        public SubscriptionToken SubscriptionToken { get; set; }

        public Action<object[]> GetExecutionStrategy()
        {
            var action = Action;
            var filter = Filter;
            if (action != null && filter != null)
            {
                return arguments =>
                    {
                        var argument = default(TPayload);
                        if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                            argument = (TPayload) arguments[0];
                        if (filter(argument))
                            InvokeAction(action, argument);
                    };
            }
            return null;
        }

        public virtual void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            action(argument);
        }
    }
}