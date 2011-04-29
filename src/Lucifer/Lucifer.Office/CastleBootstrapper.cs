using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using Caliburn.Micro;
using Castle.Core;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Lucifer.Caliburn;
using Lucifer.DataAccess;
using Lucifer.DataAccess.Configuration;
using Lucifer.DataAccess.Container;
using Lucifer.DataAccess.Persistence;
using Lucifer.Office.ViewModel;
using Parameter = Castle.MicroKernel.Registration.Parameter;

namespace Lucifer.Office
{
    public class CastleBootstrapper : Bootstrapper<IShell>
    {
        IWindsorContainer _container;

        protected override object GetInstance(Type serviceType, string key)
        {
            var export = string.IsNullOrEmpty(key) ? _container.Resolve(serviceType) : _container.Resolve(serviceType, key);
            if (export != null)
                return export;

            var contract = string.IsNullOrEmpty(key) ? serviceType.Name : key;
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            yield return _container.ResolveAll(service).GetEnumerator();
        }

        protected override void Configure()
        {
            _container = new WindsorContainer();
            _container.AddFacility<ArrayDependencyFacility>();
            _container.AddFacility("logging", new LoggingFacility(LoggerImplementation.Log4net, "log4net.config"));

            GetCaliburnRegistrations().ForEach(x => _container.Register(x));
            GetPersistenceRegistrations().ForEach(x => _container.Register(x));
            GetModuleRegistrations().ForEach(x => _container.Register(x));
            GetShellRegistrations().ForEach(x => _container.Register(x));

            _container.Install(_container.ResolveAll<IWindsorInstaller>());

            LogManager.GetLog = type => new CaliburnLogger(type);

            base.Configure();
        }

        static IEnumerable<IRegistration> GetShellRegistrations()
        {
            yield return Component
                .For<IShell>()
                .ImplementedBy<ShellViewModel>();
        }

        IEnumerable<IRegistration> GetCaliburnRegistrations()
        {
            yield return Component
                .For<IWindowManager>()
                .ImplementedBy<WindowManager>();
            yield return Component
                .For<IEventAggregator>()
                .ImplementedBy<EventAggregator>();
            yield return Component
                .For<IWindsorContainer>()
                .Instance(_container);
        }

        static IEnumerable<IRegistration> GetPersistenceRegistrations()
        {
            yield return Component
                 .For<IPersistenceConfiguration>()
                 .ImplementedBy<SqlServerPersistenceConfiguration>()
                 .Parameters(Parameter.ForKey("connectionString").Eq(ConfigurationManager.AppSettings["DbConnection"]));

            yield return AllTypes
                .FromAssemblyContaining(typeof (IMappingContributor))
                .BasedOn(typeof (IMappingContributor))
                .WithService.Base();

            yield return Component
                .For<INHibernatePersistenceModel>()
                .ImplementedBy<NHibernatePersistenceModel>();

            yield return AllTypes
                .FromAssemblyContaining(typeof (INHibernateInitializationAware))
                .BasedOn(typeof (INHibernateInitializationAware))
                .WithService.Base();

            yield return Component
                .For<INHibernateSessionFactory>()
                .ImplementedBy<NHibernateSessionFactory>();

            yield return Component
                .For<IDbConversation>()
                .ImplementedBy<DbConversation>()
                .LifeStyle.Transient;
        }

        static IEnumerable<IRegistration> GetModuleRegistrations()
        {
            yield return AllTypes
               .FromAssembly(Assembly.GetExecutingAssembly())
               .BasedOn<IModule>()
               .WithService.FromInterface(typeof(IModule));

            var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var dllPath in Directory.GetFiles(directoryPath, "*.dll"))
            {
                Assembly assembly;
                try
                {
                    assembly = Assembly.LoadFrom(dllPath);
                    AssemblySource.Instance.Add(assembly);
                }
                catch (BadImageFormatException ex)
                {
                    System.Diagnostics.Trace.TraceError(ex.ToString());
                    continue;
                }
                catch (FileNotFoundException ex)
                {
                    System.Diagnostics.Trace.TraceError(ex.ToString());
                    continue;
                }
                yield return AllTypes
                    .FromAssembly(assembly)
                    .BasedOn<IModule>()
                    .WithService.FromInterface(typeof(IModule));
                yield return AllTypes
                    .FromAssembly(assembly)
                    .BasedOn<IWindsorInstaller>()
                    .WithService.FromInterface(typeof(IWindsorInstaller));
            }
       
        }
    }
}