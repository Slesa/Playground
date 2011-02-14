using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Godot.Infrastructure;
using Godot.Infrastructure.Configuration;

namespace Godot.IcsNHibernate
{
    public class IcsMappingsRegistrationsContributor : IRegistrationContributor
    {
        public IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IMappingContributor>()
                .ImplementedBy<FluentMappingsFromAssembly>()
                .Parameters(Parameter.ForKey("assembly").Eq(GetType().Assembly.CodeBase))
                .Named("IcsMappingsFromAssembly");
        }
    }
}