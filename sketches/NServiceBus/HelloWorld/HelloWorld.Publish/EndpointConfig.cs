using NServiceBus;

namespace HelloWorld.Publish
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher
    {
    }
}
