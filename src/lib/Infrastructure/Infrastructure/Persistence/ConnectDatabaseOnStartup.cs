using NHibernate;

namespace Infrastructure.Persistence
{
    public class ConnectDatabaseOnStartup : INHibernateInitializationAware
    {
        public void BeforeInitialization() { }
        public void Configuring(NHibernate.Cfg.Configuration configuration) { }
        public void Configured(NHibernate.Cfg.Configuration configuration) { }

        public void Initialized(NHibernate.Cfg.Configuration configuration, ISessionFactory sessionFactory)
        {
            var factory = sessionFactory as NHibernateSessionFactory;
            if (factory == null) return;
            factory.Configure();
        }
    }
}