using System;
using System.Collections.Generic;
using System.Linq;

namespace Godot.IcsEditor.Ui.Events
{
    public class EventAggregator : IEventAggregator
    {
        readonly List<EventBase> _events = new List<EventBase>();

        public TEventType GetEvent<TEventType>() where TEventType : EventBase
        {
            var eventInstance = _events.FirstOrDefault(evt => evt.GetType() == typeof (TEventType)) as TEventType;
            if (eventInstance == null)
            {
                eventInstance = Activator.CreateInstance<TEventType>();
                _events.Add(eventInstance);
            }
            return eventInstance;
        }
    }
}