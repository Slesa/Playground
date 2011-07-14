using System;
using System.Diagnostics;
using HelloWorld.Core;
using NServiceBus;

namespace HelloWorld.Publish
{
    public class ServerEndpoint : IWantToRunAtStartup
    {
        public IBus Bus { get; set; }
        public Stopwatch stopWatch = new Stopwatch();
        
        public void Run()
        {
            Console.WriteLine("Server has been started...");
            stopWatch.Start();

            var publishIEvent = true;
            for (; ; )
            {
                var input = Console.ReadLine();
                if (input == null) break;

                var eventMessage = publishIEvent ? Bus.CreateInstance<IEvent>() : new HelloMessage();

                eventMessage.EventId = Guid.NewGuid();
                eventMessage.Time = DateTime.Now.Second > 30 ? (DateTime?) DateTime.Now : null;
                eventMessage.Duration = stopWatch.Elapsed;
                eventMessage.Text = input;

                Bus.Publish(eventMessage);
                Console.WriteLine("Published event with Id {0}.", eventMessage.EventId);

                publishIEvent = !publishIEvent;
                stopWatch.Start();
            }

        }

        public void Stop()
        {
            Console.WriteLine("...Server has been stopped...");
        }
    }
}