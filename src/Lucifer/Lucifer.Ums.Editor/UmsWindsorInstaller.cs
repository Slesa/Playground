using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Lucifer.Ums.Editor
{
    public class UmsWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            GetRegistrations().ForEach(x => container.Register(x));
        }

        static IEnumerable<IRegistration> GetRegistrations()
        {
            yield return AllTypes
               .FromAssemblyContaining(typeof (UmsWindsorInstaller))
               .BasedOn<IUmsModule>()
               .WithService.FromInterface(typeof(IUmsModule));

        }
    }
}