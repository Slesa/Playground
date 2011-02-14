using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using Caliburn.Micro;
using MediaOwl.Core;
using MediaOwl.Core.Logging;
using MediaOwl.NetflixServiceReference;
using MediaOwl.Services;

namespace MediaOwl
{
    /// <summary>
    /// The bootstrapper of the application
    /// This bootstrapper was taken from <see cref="http://caliburnmicro.codeplex.com/wikipage?title=Customizing%20The%20Bootstrapper"/>
    /// It inherits from <see cref="Bootstrapper&lt;TRootModel&gt;"/>.
    /// The TRootModel is <see cref="IShell"/>.
    /// This class is instanciated in the .xaml-file of the <see cref="App"/>-class.
    /// </summary>
    public class MediaOwlBootstrapper : Bootstrapper<IShell>
    {
        private CompositionContainer container;
        
        public MediaOwlBootstrapper()
        {
            LogManager.GetLog = type => new DebugLog(type);
            ConventionManager.AddElementConvention<TimeUpDown>(TimeUpDown.ValueProperty, "Value", "ValueChanged");
            ConventionManager.AddElementConvention<TimePicker>(TimePicker.ValueProperty, "Value", "ValueChanged");
        }

        protected override void Configure()
        {
            container = CompositionHost.Initialize(
            new AggregateCatalog(
                AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                )
            );

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(new NetflixCatalog(new Uri(NetflixDataAccess.BaseUri)));
            batch.AddExportedValue(container);

            container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }
    }
}