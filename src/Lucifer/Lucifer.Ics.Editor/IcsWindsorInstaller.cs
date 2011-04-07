using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Lucifer.Ics.Editor
{
    public class IcsWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            GetRegistrations().ForEach(x => container.Register(x));
        }

        static IEnumerable<IRegistration> GetRegistrations()
        {
            yield return AllTypes
               .FromAssemblyContaining(typeof (IcsWindsorInstaller))
               .BasedOn<IIcsModule>()
               .WithService.FromInterface(typeof(IIcsModule));

        }
    }
}