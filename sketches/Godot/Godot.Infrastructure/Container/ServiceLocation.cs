using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.Facilities.Logging;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Releasers;
using Castle.Windsor;
using Microsoft.Practices.ServiceLocation;

namespace Godot.Infrastructure.Container
{
    public static class ServiceLocation
    {
        public static IWindsorContainer Install()
        {
            var container = new WindsorContainer();
            container.Kernel.ReleasePolicy = new NoTrackingReleasePolicy();

            container.Register(Component.For<IWindsorContainer>().Instance(container));

//            customContainerRegistrations(container);
//            container.Install(installers.ToArray());
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

#if NEVER
        public IEnumerable<IRegistration> GetRegistrationsViaConvention()
        {
            // Zuerst die Datenbank-Anbindung
            yield return AllTypes
                .FromAssemblyContaining(typeof(IPersistenceConfiguration))
                .BasedOn(typeof(IPersistenceConfiguration))
                .WithService.FirstInterface();

            // Alle die Mapping beeinflussende Instanzen
            yield return AllTypes
                .FromAssemblyContaining(typeof(IMappingContributor))
                .BasedOn(typeof(IMappingContributor));

            yield return AllTypes
                .FromAssemblyContaining(typeof(ISessionFactory))
                .BasedOn(typeof(ISessionFactory))
                .WithService.FirstInterface();
            //.Configure(rep => rep.LifeStyle.Singleton);

            yield return AllTypes
                .FromAssemblyContaining(typeof(IRepository<>))
                .BasedOn(typeof(IRepository<>))
                .WithService.FirstInterface();
                //.Configure(rep => rep.LifeStyle.Transient);

            yield return AllTypes
                .FromAssemblyContaining<IRule>()
                .BasedOn<IRule>()
                .WithService.FirstInterface();

            /*Assembly runtime = typeof(IRule).Assembly;

            AllTypes.Of(typeof(IMapper<,>))
                .FromAssembly(runtime)
                .WithService.Select((type, baseType) => DeepestInterfaceImplementation(type))
                .Configure(r => r.LifeStyle.Is(LifestyleType.Transient));
            */
        }
#endif

#if NEVER
        static IEnumerable<IRegistration> GetRegistrations()
        {
            yield return Component
                .For<IUnitOfWork>()
                .Forward<IUnitOfWorkNh>()
                .ImplementedBy<UnitOfWorkNh>()
                .LifeStyle.Is(LifestyleType.Transient);

            yield return Component
                .For(typeof(IRepository<>))
                .ImplementedBy(typeof(RepositoryNh<>))
                .LifeStyle.Is(LifestyleType.Transient);
            /*			
            yield return Component
                .For<INHibernateSessionFactory>()
                .ImplementedBy<NHibernateSessionFactory>()
                .DependsOn(Parameter.ForKey("ConnectionString").Eq(ConfigurationManager.AppSettings["foo"]))
                .LifeStyle.Is(LifestyleType.Transient));
*/
        }
#endif
    }

    public class WindsorServiceLocator : ServiceLocatorImplBase
    {
        readonly IWindsorContainer _container;

        public WindsorServiceLocator(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>().AsEnumerable();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (key != null && key.Trim().Length > 0)
            {
                return _container.Resolve(key, serviceType);
            }
            return _container.Resolve(serviceType);
        }
    }
}