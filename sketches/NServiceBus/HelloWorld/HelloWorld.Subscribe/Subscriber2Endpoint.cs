using HelloWorld.Core;
using NServiceBus;

namespace HelloWorld.Subscribe
{
    public class Subscriber2Endpoint : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }

        public void Run()
        {
            Bus.Subscribe<IEvent>();
        }

        public void Stop()
        {
            Bus.Unsubscribe<IEvent>();
        }
    }
}