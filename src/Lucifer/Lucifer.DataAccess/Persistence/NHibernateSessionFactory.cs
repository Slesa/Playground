using System;
using System.Diagnostics;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Lucifer.DataAccess.Persistence
{
    public class NHibernateSessionFactory : INHibernateSessionFactory
    {
        static readonly object InitializationSynchronization = new object();
        readonly IPersistenceConfiguration _persistenceConfiguration;
        readonly INHibernatePersistenceModel _persistenceModel;
        ISessionFactory _sessionFactory;

        public NHibernateSessionFactory(IPersistenceConfiguration persistenceConfiguration,
                                        INHibernatePersistenceModel persistenceModel)
        {
            _persistenceConfiguration = persistenceConfiguration;
            _persistenceModel = persistenceModel;
        }

        public INHibernateInitializationAware[] Initializers
        {
            get;
            set;
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

        ~NHibernateSessionFactory()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool @explicit)
        {
            if (@explicit)
            {
                if (_sessionFactory != null)
                {
                    _sessionFactory.Dispose();
                    _sessionFactory = null;
                }
            }
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

        [Conditional("Debug")]
        static void CreateDatabaseWhenDebug(FluentConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            configuration.ExposeConfiguration(
                config => new SchemaUpdate(config).Execute(false, true));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Configure()
        {
            CreateSessionFactory();
        }
    }
}