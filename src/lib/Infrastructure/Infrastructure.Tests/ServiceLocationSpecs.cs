using System.Collections.Generic;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Infrastructure.Container;
using Machine.Specifications;

namespace Infrastructure.Tests
{
    [Subject(typeof(ServiceLocation))]
    public class When_installing_service_location
    {
        Establish context = () =>
            {
                _container = ServiceLocation.Install();
            };

        Because of = () =>
            {
                _logger = _container.Resolve<ILogger>();
            };

        It should_resolve_log = () => _logger.ShouldNotBeNull();
        It should_log_messages = () => _logger.Info("Info logging");

        static IWindsorContainer _container;
        static ILogger _logger;
    }

    [Subject(typeof (ServiceLocation))]
    public class When_resolving_per_property
    {
        public class AnyClass : IRegistrationContributor
        {
            public ILogger Log { get; set; }

            public IEnumerable<IRegistration> GetRegistrations()
            {
                yield break;
            }
        }

        Establish context = () =>
        {
            _container = ServiceLocation.Install();
        };

        Because of = () =>
            {
                _anyClass = _container.Resolve<AnyClass>();
            };

        It should_resolve_the_class = () => _anyClass.ShouldNotBeNull();
        It should_resolve_the_log = () => _anyClass.Log.ShouldNotBeNull();

        static IWindsorContainer _container;
        static AnyClass _anyClass;
    }
}