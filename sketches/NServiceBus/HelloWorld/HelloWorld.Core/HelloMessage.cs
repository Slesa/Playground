using System;
using NServiceBus;

namespace HelloWorld.Core
{
    public interface IEvent : IMessage
    {
        Guid EventId { get; set; }
        DateTime? Time { get; set; }
        TimeSpan Duration { get; set; }
        string Text { get; set; }
    }

    [Serializable]
    public class HelloMessage : IEvent
    {
        public Guid EventId { get; set; }
        public DateTime? Time { get; set; }
        public TimeSpan Duration { get; set; }
        public string Text { get; set; }
    }
}
