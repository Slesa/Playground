using System;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Modules.Wpf;

namespace ModuleB.Wpf
{
    [Module(ModuleName=WellKnownModuleNames.ModuleB, OnDemand = true)]
    public class ModuleB : IModule
    {
        readonly ILoggerFacade _logger;
        readonly IModuleTracker _moduleTracker;

        public ModuleB(ILoggerFacade logger, IModuleTracker moduleTracker)
        {
            if( logger==null )
                throw new ArgumentNullException("logger");
            _logger = logger;

            if( moduleTracker==null )
                throw new ArgumentNullException("moduleTracker");
            _moduleTracker = moduleTracker;
            _moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleB);
        }

        public void Initialize()
        {
            _logger.Log("ModuleB is initializing...", Category.Info, Priority.Medium);
            _moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleB);
            _logger.Log("ModuleB is initialized", Category.Info, Priority.Medium);
        }
    }
}
