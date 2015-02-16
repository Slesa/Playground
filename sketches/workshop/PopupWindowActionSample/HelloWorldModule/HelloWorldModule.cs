using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace HelloWorldModule
{
    public class HelloWorldModule : IModule
    {
        private readonly IRegionViewRegistry regionViewRegistry;

        public HelloWorldModule(IRegionViewRegistry registry)
        {
            this.regionViewRegistry = registry;   
        }

        public void Initialize()
        {
            this.regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof(Views.HelloWorldView));
        }
    }
}
