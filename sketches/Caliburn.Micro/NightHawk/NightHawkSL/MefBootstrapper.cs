using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using Caliburn.Micro;
using NightHawkSL.Core;
using NightHawkSL.Core.Logging;

namespace NightHawkSL
{
    public class MefBootstrapper : Bootstrapper<IShell>
    {
        private CompositionContainer _container;
        
        public MefBootstrapper()
        {
            LogManager.GetLog = type => new DebugLog(type);
            ConventionManager.AddElementConvention<TimeUpDown>(TimeUpDown.ValueProperty, "Value", "ValueChanged");
            ConventionManager.AddElementConvention<TimePicker>(TimePicker.ValueProperty, "Value", "ValueChanged");
        }

        protected override void Configure()
        {
            _container = CompositionHost.Initialize(
            new AggregateCatalog(
                AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                )
            );

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            //batch.AddExportedValue(new NetflixCatalog(new Uri(NetflixDataAccess.BaseUri)));
            batch.AddExportedValue(_container);

            _container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instance of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }
    }
}