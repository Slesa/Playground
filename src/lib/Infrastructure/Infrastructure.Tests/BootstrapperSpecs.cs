using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Infrastructure.Container;
using Machine.Specifications;

namespace Infrastructure.Tests
{
    [Subject(typeof(Bootstrapper))]
    public class When_creating_bootstrapper
    {
        public class MyPrepareStartup: IPrepareStartup
        {
            public void Prepare() { Prepared = true; }
            public bool Prepared { get; set; }
        }

        public class MyRequireConfigurationOnStartup : IRequireConfigurationOnStartup
        {
            public bool Configured { get; set; }
            public void Configure() { Configured = true; }
        }

        public class MyRegisterComponentsOnStartup : IRegisterComponentsOnStartup
        {
            public bool Configured { get; set; }
            public void Configure() { Configured = true; }
        }

        public class BootstrapperModule : IRegistrationContributor
        {
            public IEnumerable<IRegistration> GetRegistrations()
            {
                yield return Component
                    .For<IPrepareStartup>()
                    .ImplementedBy<MyPrepareStartup>();

                yield return Component
                    .For<IRequireConfigurationOnStartup>()
                    .ImplementedBy<MyRequireConfigurationOnStartup>();

                yield return Component
                    .For<IRegisterComponentsOnStartup>()
                    .ImplementedBy<MyRegisterComponentsOnStartup>();
            }
        }

        Establish context = () =>
            {
                _bootstrapper = Bootstrapper.CreateBootstrapper();
            };

        Because of = () =>
            {
                _myPrepareStartup = (MyPrepareStartup) _bootstrapper.Container.Resolve<IPrepareStartup>();
                _myRequireConfigurationOnStartup = (MyRequireConfigurationOnStartup) _bootstrapper.Container.Resolve<IRequireConfigurationOnStartup>();
                _myRegisterComponentsOnStartup = (MyRegisterComponentsOnStartup) _bootstrapper.Container.Resolve<IRegisterComponentsOnStartup>();
            };

        It should_find_prepare_startup = () => _myPrepareStartup.ShouldNotBeNull();
        It should_call_prepare_startup = () => _myPrepareStartup.Prepared.ShouldEqual(true);
        It should_find_require_configuration = () => _myRequireConfigurationOnStartup.ShouldNotBeNull();
        It should_call_require_configuration = () => _myRequireConfigurationOnStartup.Configured.ShouldEqual(true);
        It should_find_register_components = () => _myRegisterComponentsOnStartup.ShouldNotBeNull();
        It should_call_register_components = () => _myRegisterComponentsOnStartup.Configured.ShouldEqual(true);

        static Bootstrapper _bootstrapper;
        static MyPrepareStartup _myPrepareStartup;
        static MyRequireConfigurationOnStartup _myRequireConfigurationOnStartup;
        static MyRegisterComponentsOnStartup _myRegisterComponentsOnStartup;
    }
 }