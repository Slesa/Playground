using System;
using HelloWorld.Core;
using NServiceBus;

namespace HelloWorld.Consume
{
    public class EventMessageHandler : IHandleMessages<HelloMessage>
    {
        public void Handle(HelloMessage message)
        {
            Console.WriteLine("received message with id {0}, time {1}, duration {2}: {3}", message.EventId, message.Time, message.Duration, message.Text);
        }
    }
}