using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using Infrastructure.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Infrastructure.Persistence
{
    public class NHibernateSessionFactory : INhibernateSessionFactory
    {
        static readonly object InitializationSynchronization = new object();
        readonly IPersistenceConfiguration _persistenceConfiguration;
        readonly INHibernatePersistenceModel _persistenceModel;
        ISessionFactory _sessionFactory;

        public NHibernateSessionFactory(IPersistenceConfiguration persistenceConfiguration, INHibernatePersistenceModel persistenceModel)
        {
            _persistenceConfiguration = persistenceConfiguration;
            _persistenceModel = persistenceModel;
        }

        public INHibernateInitializationAware[] Initializers
        {
            get;
            set;
        }

        ~NHibernateSessionFactory()
        {
            Dispose(false);
        }

        public void Configure()
        {
            _sessionFactory = CreateSessionFactory();
        }

        public ISession CreateSession()
        {
            if (_sessionFactory == null)
            {
                lock (InitializationSynchronization)
                {
                    if (_sessionFactory == null)
                    {
                        _sessionFactory = CreateSessionFactory();
                    }
                }
            }

            var session = _sessionFactory.OpenSession();
            session.FlushMode = FlushMode.Commit;
            return session;
        }

        public void Dispose()
        {
            Dispose(true);
            // Prevent the destructor from being called.
            GC.SuppressFinalize(this);
        }

        ISessionFactory CreateSessionFactory()
        {
            Initializers.Each(x => x.BeforeInitialization());

            var configuration = Fluently.Configure()
                .Database(_persistenceConfiguration.GetConfiguration())
                .Mappings(_persistenceModel.AddMappings);

            configuration.ExposeConfiguration(c => Initializers.Each(x => x.Configuring(c)));

            var actualConfiguration = configuration.BuildConfiguration();
            Initializers.Each(x => x.Configured(actualConfiguration));
            CreateDatabaseWhenDebug(configuration);

            _sessionFactory = configuration.BuildSessionFactory();

            Initializers.Each(x => x.Initialized(actualConfiguration, _sessionFactory));

            return _sessionFactory;
        }

        static void CreateDatabaseWhenDebug(FluentConfiguration configuration)
        {
#if DEBUG
            if (configuration == null) throw new ArgumentNullException("configuration");
            configuration.ExposeConfiguration(
                config => new SchemaUpdate(config).Execute(false, true));
#endif
        }

        protected virtual void Dispose(bool @explicit)
        {
            if (!@explicit) return;
            if (_sessionFactory == null) return;
            _sessionFactory.Dispose();
            _sessionFactory = null;
        }
    }
}