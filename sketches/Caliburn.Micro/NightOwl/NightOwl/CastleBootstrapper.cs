using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NightOwl.Core;
using NightOwl.ViewModels;

namespace NightOwl
{
    // JetPack Theme for WPF:
    // http://blog.lexique-du-net.com/index.php?post/2010/10/14/Migrate-the-Jetpack-theme-from-Silverlight-to-WPF
    public class CastleBootstrapper : Bootstrapper<IShell>
    {
        IWindsorContainer _container;

        protected override void Configure()
        {
            _container = new WindsorContainer();

            GetCoreRegistrations().ForEach(x => _container.Register(x));;
            GetModuleRegistrations().ForEach(x => _container.Register(x));;
            GetShellRegistrations().ForEach(x => _container.Register(x));;
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var export = string.IsNullOrEmpty(key) ? _container.Resolve(serviceType) : _container.Resolve(key);
            if (export != null)
                return export;

            var contract = string.IsNullOrEmpty(key) ? serviceType.Name : key;
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            yield return _container.ResolveAll(service).GetEnumerator();
        }

        //protected override void BuildUp(object instance)
        //{
        //    //_container.Resolve(typeof(instance));
        //    //_container.SatisfyImportsOnce(instance);
        //}

        IEnumerable<IRegistration> GetCoreRegistrations()
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

        static IEnumerable<IRegistration> GetShellRegistrations()
        {
            yield return Component
                .For<IShell>()
                .ImplementedBy<ShellViewModel>();
        }


        static IEnumerable<IRegistration> GetModuleRegistrations()
        {
            yield return AllTypes
                .FromAssembly(Assembly.GetExecutingAssembly())
                .BasedOn<IModule>()
                .WithService.FromInterface(typeof (IModule));

            var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var dllPath in Directory.GetFiles(directoryPath, "*.dll"))
            {
                Assembly assembly;
                try
                {
                    assembly = Assembly.LoadFrom(dllPath);
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
            }
        }
    }
}