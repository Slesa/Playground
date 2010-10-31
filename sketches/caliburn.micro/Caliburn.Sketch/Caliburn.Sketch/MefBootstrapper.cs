using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Caliburn.Micro;

namespace Caliburn.Sketch
{
    public class MefBootstrapper : Bootstrapper<IShell>
    {
        CompositionContainer _container;

        protected override void Configure()
        {
#if SILVERLIGHT
            _container =
                CompositionHost.Initialize(
                    new AggregateCatalog(
                        AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));
#else
            var directoryCatalog = new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);
            _container = new CompositionContainer(directoryCatalog);
#endif

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);

            _container.Compose(batch);
        }

        protected override object GetInstance(Type service, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instance of contract {0}", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }
    }
}