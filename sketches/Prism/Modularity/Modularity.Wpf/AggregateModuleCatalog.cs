using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;

namespace Modularity.Wpf
{
    public class AggregateModuleCatalog : IModuleCatalog
    {
        List<IModuleCatalog> _catalogs = new List<IModuleCatalog>();

        public AggregateModuleCatalog()
        {
            _catalogs.Add(new ModuleCatalog());
        }

        public ReadOnlyCollection<IModuleCatalog> Catalogs
        {
            get { return _catalogs.AsReadOnly(); }
        }

        public void AddCatalog(IModuleCatalog catalog)
        {
            if( catalog==null )
                throw new ArgumentNullException("catalog");
            _catalogs.Add(catalog);
        }

        public IEnumerable<ModuleInfo> Modules
        {
            get { return Catalogs.SelectMany(x => x.Modules); }
        }

        public IEnumerable<ModuleInfo> GetDependentModules(ModuleInfo moduleInfo)
        {
            var catalog = _catalogs.Single(x => x.Modules.Contains(moduleInfo));
            return catalog.GetDependentModules(moduleInfo);
        }

        public IEnumerable<ModuleInfo> CompleteListWithDependencies(IEnumerable<ModuleInfo> modules)
        {
            var groupedModules =
                modules.GroupBy(module => _catalogs.Single(catalog => catalog.Modules.Contains(module)));
            return groupedModules.SelectMany(x => x.Key.CompleteListWithDependencies(x));
        }

        public void Initialize()
        {
            foreach(var catalog in Catalogs)
                catalog.Initialize();
        }

        public void AddModule(ModuleInfo moduleInfo)
        {
            _catalogs[0].AddModule(moduleInfo);
        }

    }
}