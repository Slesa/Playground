using System;
using System.Collections.Generic;
using System.Linq;

namespace Godot.IcsEditor.Ui.Events
{
    public abstract class EventBase
    {
        readonly List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        protected ICollection<IEventSubscription> Subscriptions
        {
            get { return _subscriptions; }
        }

        protected virtual SubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
        {
            eventSubscription.SubscriptionToken = new SubscriptionToken();
            lock (Subscriptions)
            {
                Subscriptions.Add(eventSubscription);
            }
            return eventSubscription.SubscriptionToken;
        }

        protected virtual void InternalPublish(params object[] arguments)
        {
            var executionStrategies = PruneAndReturnStrategies();
            foreach (var executionStrategy in executionStrategies)
            {
                executionStrategy(arguments);
            }
        }

        public virtual void Unsubscribe(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                var subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                if (subscription != null)
                    Subscriptions.Remove(subscription);
            }
        }

        public virtual bool Contains(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                var subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                return subscription != null;
            }
        }

        private IEnumerable<Action<object[]>> PruneAndReturnStrategies()
        {
            var resultList = new List<Action<object[]>>();
            lock (Subscriptions)
            {
                for (var i = Subscriptions.Count - 1; i >= 0; i--)
                {
                    var listItem = _subscriptions[i].GetExecutionStrategy();
                    if( listItem==null)
                        _subscriptions.RemoveAt(i);
                    else
                        resultList.Add(listItem);
                }
            }
            return resultList;
        }
    }
}