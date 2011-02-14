using System.Collections.Generic;
using System.Configuration;
using Castle.MicroKernel.Registration;
using Godot.Infrastructure.Persistence;
using Godot.Model;

namespace Godot.Infrastructure.Configuration
{
    public class InfrastructureRegistrationContributor : IRegistrationContributor
    {
        public IEnumerable<IRegistration> GetRegistrations()
        {
            // Zuerst die Datenbank-Anbindung
            yield return Component
                .For<IPersistenceConfiguration>()
                .ImplementedBy<SQLServerPersistenceConfiguration>()
                .Parameters(Parameter.ForKey("connectionString").Eq(ConfigurationManager.AppSettings["DbConnection"]));

            // Alle die Mapping beeinflussende Instanzen
            yield return AllTypes
                .FromAssemblyContaining(typeof (IMappingContributor))
                .BasedOn(typeof (IMappingContributor))
                .WithService.Base();

            // Dann die Mapping-Konfiguration selbst
            yield return Component
                .For<INHibernatePersistenceModel>()
                .ImplementedBy<NHibernatePersistenceModel>();

            // NHibernate-Initializierungs-Komponenten/Listeners
            yield return AllTypes
                .FromAssemblyContaining(typeof (INHibernateInitializationAware))
                .BasedOn(typeof (INHibernateInitializationAware))
                .WithService.Base();

            // Die NH SessionFactory
            yield return Component
                .For<INHibernateSessionFactory>()
                .ImplementedBy<NHibernateSessionFactory>();

            // Die NH SessionFactory
            yield return Component
                .For<IDbConversation>()
                .ImplementedBy<DbConversation>()
                .LifeStyle.Transient;
        }

    }
}