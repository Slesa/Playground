using DataAccess;
using Infrastructure.Persistence;
using Ninject.Modules;

namespace Infrastructure.Configuration
{
    public class InfrastructureNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfigurationReader>().To<AppSettingsConfigurationReader>().InSingletonScope();

            Bind<IPersistenceConfiguration>().To<SqlServerConfiguration>().InSingletonScope();
            Bind<IMappingContributor>().To<FluentMappingConventions>().InSingletonScope();

            Bind<INHibernateInitializationAware>().To<ConnectDatabaseOnStartup>();
            Bind<INHibernateInitializationAware>().To<NHibernateProfilerInitializer>();

            Bind<INHibernatePersistenceModel>().To<NHibernatePersistenceModel>().InSingletonScope();

            Bind<INhibernateSessionFactory>().To<NHibernateSessionFactory>().InSingletonScope();

            Bind<IDbConversation>().To<DbConversation>().InTransientScope();
        }
    }
}