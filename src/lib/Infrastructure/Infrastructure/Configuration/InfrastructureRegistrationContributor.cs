using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using DataAccess;
using Infrastructure.Container;
using Infrastructure.Persistence;

namespace Infrastructure.Configuration
{
    public class InfrastructureRegistrationContributor : IRegistrationContributor
    {
        public IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IConfigurationReader>()
                .ImplementedBy<AppSettingsConfigurationReader>();

            yield return Component
                .For<IPersistenceConfiguration>()
                .ImplementedBy<SqlServerConfiguration>();

            yield return AllTypes
                .FromAssemblyContaining(typeof (IMappingContributor))
                .BasedOn(typeof (IMappingContributor))
                .WithService.Base();

            yield return Component
                .For<INHibernatePersistenceModel>()
                .ImplementedBy<NHibernatePersistenceModel>();

            yield return AllTypes
                .FromAssemblyContaining(typeof (INHibernateInitializationAware))
                .BasedOn(typeof (INHibernateInitializationAware))
                .WithService.Base();

            // Die NH SessionFactory
            yield return Component
                .For<INhibernateSessionFactory>()
                .ImplementedBy<NHibernateSessionFactory>();

            // Die NH SessionFactory
            yield return Component
                .For<IDbConversation>()
                .ImplementedBy<DbConversation>()
                .LifeStyle.Transient;
        }
    }
}