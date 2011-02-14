using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Utils;
using Godot.Infrastructure.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Godot.Infrastructure.Persistence
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

        static void CreateDatabaseWhenDebug(FluentConfiguration configuration)
        {
#if DEBUG
            if (configuration == null) throw new ArgumentNullException("configuration");
            configuration.ExposeConfiguration(
                config => new SchemaUpdate(config).Execute(false, true));
#endif
        }

        /// <summary>
        ///	Finalizes an instance of the <see cref="NHibernateSessionFactory" /> class.
        /// </summary>
        /// <remarks>
        ///	This destructor will run only if the Dispose method does not get called.
        ///	The destructor is called indeterministicly by the Garbage Collector.
        /// </remarks>
        ~NHibernateSessionFactory()
        {
            // Since other managed ob jects are disposed automatically,
            // we should not try to dispose any managed resources.
            // We therefore pass false to Dispose().
            Dispose(false);
        }

        /// <summary>
        ///	Central method for cleaning up resources.
        /// </summary>
        protected virtual void Dispose(bool @explicit)
        {
            // If explicit is true, then this method was called through
            // the public Dispose().
            if (@explicit)
            {
                // Release or cleanup managed resources.
                if (_sessionFactory != null)
                {
                    _sessionFactory.Dispose();
                    _sessionFactory = null;
                }
            }

            // Always release or cleanup (any) unmanaged resources.
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

        /// <summary>
        ///	Cleans up managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // Prevent the destructor from being called.
            GC.SuppressFinalize(this);
        }

        public void Configure()
        {
            CreateSessionFactory();
        }
    }
}