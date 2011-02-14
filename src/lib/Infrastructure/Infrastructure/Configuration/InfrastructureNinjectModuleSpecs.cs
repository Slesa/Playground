using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Infrastructure.Persistence;
using Machine.Specifications;
using Ninject;

namespace Infrastructure.Configuration
{
    [Subject(typeof(InfrastructureNinjectModule))]
    public class When_resolving_injected_stuff
    {
        Establish context = () =>
            {
                _bootstrapper = Bootstrapper.CreateBootstrapper();
            };

        Because of = () =>
            {
                _kernel = _bootstrapper.Container.Get<IKernel>();
                _configurationReader = _bootstrapper.Container.Get<IConfigurationReader>();
                _persistanceConfiguration = _bootstrapper.Container.Get<IPersistenceConfiguration>();
                _mappingContributor = _bootstrapper.Container.Get<IMappingContributor>();

                _persistenceModel = _bootstrapper.Container.Get<INHibernatePersistenceModel>();
                _sessionFactory = _bootstrapper.Container.Get<INhibernateSessionFactory>();
                _dbConversation = _bootstrapper.Container.Get<IDbConversation>();

                _nhibernateInitializationAware = _bootstrapper.Container.GetAll<INHibernateInitializationAware>();
            };

        It should_resolve_kernel = () => _kernel.ShouldNotBeNull();
        It should_resolve_configuration_reader = () => _configurationReader.ShouldNotBeNull();
        It should_resolve_persistence_configuration = () => _persistanceConfiguration.ShouldNotBeNull();
        It should_resolve_mapping_contributor = () => _mappingContributor.ShouldNotBeNull();
        It should_resolve_persistence_model = () => _persistenceModel.ShouldNotBeNull();
        It should_resolve_session_factory = () => _sessionFactory.ShouldNotBeNull();
        It should_resolve_db_conversation = () => _dbConversation.ShouldNotBeNull();

        It should_resolve_all_nhibernate_initialization_aware =
            () => _nhibernateInitializationAware.Count().ShouldEqual(2);

        static Bootstrapper _bootstrapper;
        static IKernel _kernel;
        static IConfigurationReader _configurationReader;
        static IPersistenceConfiguration _persistanceConfiguration;
        static IMappingContributor _mappingContributor;
        static INHibernatePersistenceModel _persistenceModel;
        static INhibernateSessionFactory _sessionFactory;
        static IDbConversation _dbConversation;
        static IEnumerable<INHibernateInitializationAware> _nhibernateInitializationAware;
    }

    [Subject(typeof(InfrastructureNinjectModule))]
    public class When_resolving_configuration_reader
    {
         Establish context = () =>
            {
                _bootstrapper = Bootstrapper.CreateBootstrapper();
                _configurationReader1 = _bootstrapper.Container.Get<IConfigurationReader>();
            };

        Because of = () =>
            {
                _configurationReader2 = _bootstrapper.Container.Get<IConfigurationReader>();
            };

        It should_be_always_the_same_instance = () => _configurationReader1.ShouldBeTheSameAs(_configurationReader2);

        static Bootstrapper _bootstrapper;
        static IConfigurationReader _configurationReader1;
        static IConfigurationReader _configurationReader2;
    }

    [Subject(typeof(InfrastructureNinjectModule))]
    public class When_resolving_persistence_configuration
    {
        Establish context = () =>
        {
            _bootstrapper = Bootstrapper.CreateBootstrapper();
            _persistenceConfiguration1 = _bootstrapper.Container.Get<IPersistenceConfiguration>();
        };

        Because of = () =>
        {
            _persistenceConfiguration2 = _bootstrapper.Container.Get<IPersistenceConfiguration>();
        };

        It should_be_always_the_same_instance = () => _persistenceConfiguration1.ShouldBeTheSameAs(_persistenceConfiguration2);

        static Bootstrapper _bootstrapper;
        static IPersistenceConfiguration _persistenceConfiguration1;
        static IPersistenceConfiguration _persistenceConfiguration2;
    }

    [Subject(typeof(InfrastructureNinjectModule))]
    public class When_resolving_mapping_contributor
    {
        Establish context = () =>
        {
            _bootstrapper = Bootstrapper.CreateBootstrapper();
            _mappingContributor1 = _bootstrapper.Container.Get<IMappingContributor>();
        };

        Because of = () =>
        {
            _mappingContributor2 = _bootstrapper.Container.Get<IMappingContributor>();
        };

        It should_be_always_the_same_instance = () => _mappingContributor1.ShouldBeTheSameAs(_mappingContributor2);

        static Bootstrapper _bootstrapper;
        static IMappingContributor _mappingContributor1;
        static IMappingContributor _mappingContributor2;
    }

    [Subject(typeof(InfrastructureNinjectModule))]
    public class When_resolving_persistence_model
    {
        Establish context = () =>
        {
            _bootstrapper = Bootstrapper.CreateBootstrapper();
            _persistenceModel1 = _bootstrapper.Container.Get<INHibernatePersistenceModel>();
        };

        Because of = () =>
        {
            _persistenceModel2 = _bootstrapper.Container.Get<INHibernatePersistenceModel>();
        };

        It should_be_always_the_same_instance = () => _persistenceModel1.ShouldBeTheSameAs(_persistenceModel2);

        static Bootstrapper _bootstrapper;
        static INHibernatePersistenceModel _persistenceModel1;
        static INHibernatePersistenceModel _persistenceModel2;
    }

    [Subject(typeof(InfrastructureNinjectModule))]
    public class When_resolving_session_factory
    {
        Establish context = () =>
        {
            _bootstrapper = Bootstrapper.CreateBootstrapper();
            _sessionFactory1 = _bootstrapper.Container.Get<INhibernateSessionFactory>();
        };

        Because of = () =>
        {
            _sessionFactory2 = _bootstrapper.Container.Get<INhibernateSessionFactory>();
        };

        It should_be_always_the_same_instance = () => _sessionFactory1.ShouldBeTheSameAs(_sessionFactory2);

        static Bootstrapper _bootstrapper;
        static INhibernateSessionFactory _sessionFactory1;
        static INhibernateSessionFactory _sessionFactory2;
    }

    [Subject(typeof(InfrastructureNinjectModule))]
    public class When_resolving_db_conversation
    {
        Establish context = () =>
        {
            _bootstrapper = Bootstrapper.CreateBootstrapper();
            _dbConversation1 = _bootstrapper.Container.Get<IDbConversation>();
        };

        Because of = () =>
        {
            _dbConversation2 = _bootstrapper.Container.Get<IDbConversation>();
        };

        It should_not_be_the_same_instance = () => _dbConversation1.ShouldNotBeTheSameAs(_dbConversation2);

        static Bootstrapper _bootstrapper;
        static IDbConversation _dbConversation1;
        static IDbConversation _dbConversation2;
    }
}