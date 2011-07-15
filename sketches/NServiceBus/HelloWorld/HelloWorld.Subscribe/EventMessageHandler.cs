using System;
using HelloWorld.Core;
using NServiceBus;

namespace HelloWorld.Subscribe
{
    public class EventMessageHandler : IHandleMessages<IEvent>
    {
        public void Handle(IEvent message)
        {
            Console.WriteLine("Received IEvent with Id {0}, time {1}, duration {2}: {3}", message.EventId, message.Time, message.Duration, message.Text);
        }
    }
}