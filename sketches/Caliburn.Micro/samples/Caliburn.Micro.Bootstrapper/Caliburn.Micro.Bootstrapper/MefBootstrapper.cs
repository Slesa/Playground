using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
#if SILVERLIGHT
using System.ComponentModel.Composition.Primitives;
#endif
using System.Linq;

namespace Caliburn.Micro.Bootstrapper
{
    public class MefBootstrapper : Bootstrapper<IShell>
    {
        CompositionContainer _container;

        protected override void Configure()
        {
#if SILVERLIGHT
            _container = CompositionHost.Initialize(
                new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                    )
                );
#else
            // TODO: funktioniert nicht, fehlt wohl der Catalog
            _container = new CompositionContainer();
#endif
            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);

            _container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override System.Collections.Generic.IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }
    }
}