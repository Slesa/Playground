using NServiceBus;

namespace HelloWorld.Subscribe
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With()
                .CastleWindsorBuilder()
                .XmlSerializer()
                .UnicastBus()
                    .DoNotAutoSubscribe();
        }
    }
}
