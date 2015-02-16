using HelloWorldModule.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace HelloWorldModule
{
    public class ClientSelectedEvent : PubSubEvent<ClientData>
    {
    }
}
