using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Lucifer.DataAccess.Configuration;
using Lucifer.DataAccess.Persistence;

namespace Lucifer.Ums.Mapping
{
    public class UmsWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            GetRegistrations().ForEach(x => container.Register(x));
        }

        static IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IMappingContributor>()
                .ImplementedBy<FluentMappingFromAssembly>()
                .Parameters(Parameter.ForKey("assembly").Eq(typeof(UserRoleMap).Assembly.CodeBase))
                .Named("UmsMappingsFromAssembly");
        }
    }
}