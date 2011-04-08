using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Lucifer.Pms.Editor
{
    public class PmsWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            GetRegistrations().ForEach(x => container.Register(x));
        }

        static IEnumerable<IRegistration> GetRegistrations()
        {
            yield return AllTypes
               .FromAssemblyContaining(typeof (PmsWindsorInstaller))
               .BasedOn<IPmsModule>()
               .WithService.FromInterface(typeof(IPmsModule));

        }
    }
}