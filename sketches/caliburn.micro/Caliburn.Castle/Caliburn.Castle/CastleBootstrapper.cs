using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Caliburn.Castle
{
    public class CastleBootstrapper : Bootstrapper<IShell>
    {
        WindsorContainer _container;

        protected override void Configure()
        {
            _container = new WindsorContainer();

            _container.Register(Component.For<IWindsorContainer>().Instance(_container));
            _container.Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>());
            _container.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>());
            _container.Register(Component.For<IShell>().ImplementedBy<ShellViewModel>());

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

        protected override object GetInstance(Type service, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? service.Name : key;
            return _container.Resolve<object>(contract);
            /*
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = _container.GetExportedValues<object>(contract);
            
            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instance of contract {0}", service));
             * */
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.ResolveAll<object>(service);
            //return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        /*
        protected override void BuildUp(object instance)
        {
            //_container.SatisfyImportsOnce(instance);
        }*/

    }
}