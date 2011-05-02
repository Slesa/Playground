using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using BugTracker.Model;
using Caliburn.Micro;

namespace BugTracker
{
    public class AppBootstrapper : Bootstrapper<IShell>
    {
        private CompositionContainer container;

        /// <summary>
        /// By default, we are configured to use MEF
        /// </summary>
        protected override void Configure()
        {
            var catalog = new AggregateCatalog(
                AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                );

            container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);
            batch.AddExportedValue(catalog);
            var bugRepository = new BugRepository();
            // bugRepository.Save(new Bug() { CreatedOn = DateTime.Now, Creator = "kev", Description = "First bug", Severity = Severity.Normal });
            // bugRepository.Save(new Bug() { CreatedOn = DateTime.Now, Creator = "kev", Description = "Second Bug", Severity = Severity.Major });
            batch.AddExportedValue<IBugRepository>(bugRepository);

            container.Compose(batch);

            LogManager.GetLog = type => new ConsoleLog(type);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            IEnumerable<object> exports = container.GetExportedValues<object>(contract);

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