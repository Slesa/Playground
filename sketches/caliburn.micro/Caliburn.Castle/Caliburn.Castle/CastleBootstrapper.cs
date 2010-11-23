using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Caliburn.Micro;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Caliburn.Castle
{
    public class CastleBootstrapper : Bootstrapper<IShell>
    {
        IWindsorContainer _windsorContainer;

        protected override void Configure()
        {
           _windsorContainer = new WindsorContainer();

           _windsorContainer.Register(Component.For<IWindsorContainer>().Instance(_windsorContainer));
           _windsorContainer.Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>());
           _windsorContainer.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>());

            /*
            _windsorContainer.Install(new CommonsWindsorInstaller(),
                                      new InfastructureInstaller(),
                                      new CaliburnMicroInstaller(),
                                      new ViewModelInstaller(),
                                      new MessageHandlerInstaller());
            */
            _windsorContainer.Register(Component.For<IShell>().ImplementedBy<ShellViewModel>().LifeStyle.Is(LifestyleType.Singleton));
            _windsorContainer.Register(Component.For<ShellView>().ImplementedBy<ShellView>());

            /*
            var eventAggregator = _windsorContainer.Resolve<IEventAggregator>();
            var messageHandlers = _windsorContainer.ResolveAll<IMessageHandler>();
            foreach (var messageHandler in messageHandlers)
            {
                eventAggregator.Subscribe(messageHandler);    
            }
            */

            /*
            _container =
                CompositionHost.Initialize(
                    new AggregateCatalog(
                        AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);

            _container.Compose(batch);
             * */
        }

#if (!SILVERLIGHT)
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
            /*
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
            }*/
        }
#endif

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key) ? _windsorContainer.Resolve(service) : _windsorContainer.Resolve(key, new { });
        }


        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _windsorContainer.ResolveAll(service).Cast<object>();
        }

#if (!SILVERLIGHT)
       protected override void BuildUp(object instance)
        {
            instance.GetType().GetProperties()
                .Where(property => property.CanWrite && property.PropertyType.IsPublic)
                .Where(property => _windsorContainer.Kernel.HasComponent(property.PropertyType))
                .ForEach(property => property.SetValue(instance, _windsorContainer.Resolve(property.PropertyType), null));
        }
#endif

    }
}