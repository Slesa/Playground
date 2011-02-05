using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils;
using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Machine.Specifications;
using NHibernate;
using Rhino.Mocks;

namespace Infrastructure.Tests
{
    [Subject(typeof (NHibernateSessionFactory))]
    public class When_a_nhibernate_session_created_for_the_first_time
    {
        static IPersistenceConfiguration _persistenceConfiguration;
        static INHibernatePersistenceModel _persistenceModel;
        static NHibernateSessionFactory _factory;
        static INHibernateInitializationAware[] _initializers;
        static ISession _session;

        Establish context = () =>
            {
                _persistenceConfiguration = MockRepository.GenerateStub<IPersistenceConfiguration>();
                _persistenceConfiguration
                    .Stub(x => x.GetConfiguration())
                    .Return(new SqLiteInMemoryConfiguration().GetConfiguration());

                _persistenceModel = MockRepository.GenerateStub<INHibernatePersistenceModel>();
                _persistenceModel
                    .Stub(x => x.AddMappings(null))
                    .IgnoreArguments()
                    .WhenCalled(x =>
                        {
                            var config = (MappingConfiguration) x.Arguments.First();
                            config.FluentMappings.Add<MappedClassMap>();
                        });

                _initializers = new[]
                    {
                        MockRepository.GenerateStub<INHibernateInitializationAware>(),
                        MockRepository.GenerateStub<INHibernateInitializationAware>()
                    };

                _factory = new NHibernateSessionFactory(_persistenceConfiguration, _persistenceModel)
                    {
                        Initializers = _initializers
                    };
            };

        Because of = () => { _session = _factory.CreateSession(); };

        It should_add_mappings_from_the_persistence_model =
            () => _persistenceModel.AssertWasCalled(x => x.AddMappings(Arg<MappingConfiguration>.Is.NotNull),
                                                    // First call connect database on startup, 2nd one nh profiler start
                                                    o => o.Repeat.Times(1,2));

        It should_be_able_to_create_a_session =
            () => _session.ShouldNotBeNull();

        It should_create_a_session_that_flushes_on_commit =
            () => _session.FlushMode.ShouldEqual(FlushMode.Commit);

        It should_invoke_the_initializers_before_initialization =
            () => _initializers.Each(x => x.AssertWasCalled(i => i.BeforeInitialization()));

        It should_invoke_the_initializers_while_configuring =
            () =>
            _initializers.Each(x => x.AssertWasCalled(i => i.Configuring(Arg<NHibernate.Cfg.Configuration>.Is.NotNull),
                                                      // First call: by the NHSF, second call by FNH.                                           
                                                      o => o.Repeat.Twice()));

        It should_invoke_the_initializers_with_the_actual_configuration =
            () =>
            _initializers.Each(x => x.AssertWasCalled(i => i.Configured(Arg<NHibernate.Cfg.Configuration>.Is.NotNull)));

        It should_invoke_the_initializers_with_the_session_factory =
            () =>
            _initializers.Each(x => x.AssertWasCalled(i => i.Initialized(Arg<NHibernate.Cfg.Configuration>.Is.NotNull,
                                                                         Arg<ISessionFactory>.Is.NotNull)));

        It should_retrieve_the_persistence_configuration =
            () => _persistenceConfiguration.AssertWasCalled(x => x.GetConfiguration());
    }

    [Subject(typeof (NHibernateSessionFactory))]
    public class When_a_nhibernate_session_created
    {
        static IPersistenceConfiguration _persistenceConfiguration;
        static INHibernatePersistenceModel _persistenceModel;
        static ISession _session;
        static NHibernateSessionFactory _factory;
        static INHibernateInitializationAware[] _initializers;

        Establish context = () =>
            {
                _persistenceConfiguration = MockRepository.GenerateStub<IPersistenceConfiguration>();
                _persistenceConfiguration
                    .Stub(x => x.GetConfiguration())
                    .Return(new SqLiteInMemoryConfiguration().GetConfiguration());

                _persistenceModel = MockRepository.GenerateStub<INHibernatePersistenceModel>();
                _persistenceModel
                    .Stub(x => x.AddMappings(null))
                    .IgnoreArguments()
                    .WhenCalled(x =>
                        {
                            var config = (MappingConfiguration) x.Arguments.First();
                            config.FluentMappings.Add<MappedClassMap>();
                        });

                _initializers = new[]
                    {
                        MockRepository.GenerateStub<INHibernateInitializationAware>()
                    };

                _factory = new NHibernateSessionFactory(_persistenceConfiguration, _persistenceModel)
                    {
                        Initializers = _initializers
                    };

                _factory.CreateSession();

                _initializers = new[]
                    {
                        MockRepository.GenerateStub<INHibernateInitializationAware>()
                    };
            };

        Because of = () => { _session = _factory.CreateSession(); };

        It should_be_able_to_create_a_session =
            () => _session.ShouldNotBeNull();

        It should_create_a_session_that_flushes_on_commit =
            () => _session.FlushMode.ShouldEqual(FlushMode.Commit);

        It should_not_reinitialize_the_session_factory =
            () => _initializers.Each(x => x.AssertWasNotCalled(i => i.BeforeInitialization()));
    }

    public class MappedClass
    {
        protected MappedClass()
        {
        }

        public virtual int Id { get; set; }
    }

    public class MappedClassMap : ClassMap<MappedClass>
    {
        public MappedClassMap()
        {
            Id(x => x.Id);
        }
    }
}