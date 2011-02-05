using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Godot.Infrastructure;
using Godot.Infrastructure.Configuration;
using Godot.PmsMatrix.Persistence;

namespace Godot.PmsMatrix
{
    public class PmsMappingsRegistrationsContributor : IRegistrationContributor
    {
        public IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IDatFileMapper>()
                .ImplementedBy<DatFileMapper>();
                //.Configure(x => x.LifeStyle.Transient);

            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(IRequireConfigurationOnStartup))
                .WithService.Base(); //.Configure(x => x.LifeStyle.Transient);

            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(IRegisterComponentsOnStartup))
                .WithService.Base(); //.Configure(x => x.LifeStyle.Transient);

            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(IPrepareStartup))
                .WithService.Base(); //.Configure(x => x.LifeStyle.Transient);

            /*
            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(ClassMap<>)); //.Configure(x => x.LifeStyle.Transient);
             */
            yield return AllTypes
                .FromAssemblyContaining(GetType())
                .BasedOn(typeof(IMatrixFileLoader<>))
                .WithService.Base();

            /*
            yield return Component
                .For<IMatrixFileLoader>()
                .ImplementedBy<DatFileLoader<T>>()
                .Configure(x => x.LifeStyle.Transient);
            */
            yield return Component
                .For<IMappingContributor>()
                .ImplementedBy<FluentMappingsFromAssembly>()
                .Parameters(Parameter.ForKey("assembly").Eq(GetType().Assembly.CodeBase))
                .Named("PmsMappingsFromAssembly");

        }
    }
}