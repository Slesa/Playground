using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.Facilities.Logging;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Releasers;
using Castle.Windsor;
using Infrastructure.Container;
using Microsoft.Practices.ServiceLocation;

namespace Infrastructure
{
    public class ServiceLocation
    {
        public static IWindsorContainer Install()
        {
            var container = new WindsorContainer();
            container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();

            container.Register(Component.For<IWindsorContainer>().Instance(container));

            container.Install(new DefaultComponentInstaller());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));

            return container;
        }

    }

    internal class DefaultComponentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //            container.AddFacility<FactorySupportFacility>();
            container.AddFacility<ArrayDependencyFacility>();
            //container.AddFacility<LoggingFacility>("logging", f => f. f.LogUsing(LoggerImplementation.Log4net).WithConfig("log4net.xml"));
            //container.AddFacility("logging", new LoggingFacility(LoggerImplementation.Console));
            container.AddFacility("logging", new LoggingFacility(LoggerImplementation.Log4net, "log4net.config"));

            GetBootstrapRegistrations(container);

            var registrationContributors = container.ResolveAll<IRegistrationContributor>();
            foreach (var registrations in
                registrationContributors.Select(registrationContributor => registrationContributor.GetRegistrations()))
            {
                container.Register(registrations.ToArray());
            }
        }

        static void GetBootstrapRegistrations(IWindsorContainer container)
        {
            var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var dllPath in Directory.GetFiles(directoryPath, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dllPath);
                    container.Register(
                        AllTypes
                        .FromAssembly(assembly)
                        .BasedOn<IRegistrationContributor>());

                }
                catch (BadImageFormatException ex)
                {
                    System.Diagnostics.Trace.TraceError(ex.ToString());
                }
                catch (FileNotFoundException ex)
                {
                    System.Diagnostics.Trace.TraceError(ex.ToString());
                }
            }

        }
    }
}