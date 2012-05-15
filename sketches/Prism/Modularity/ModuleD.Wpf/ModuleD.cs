using System;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Modules.Wpf;

namespace ModuleD.Wpf
{
    public class ModuleD : IModule
    {
        readonly ILoggerFacade _logger;
        readonly IModuleTracker _moduleTracker;

        public ModuleD(ILoggerFacade logger, IModuleTracker moduleTracker)
        {
            if( logger==null )
                throw new ArgumentNullException("logger");
            _logger = logger;

            if(moduleTracker==null)
                throw new ArgumentNullException("moduleTracker");
            _moduleTracker = moduleTracker;
            _moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleD);
        }

        public void Initialize()
        {
            _logger.Log("ModuleD is initializing...", Category.Info, Priority.Medium);
            _moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleD);
            _logger.Log("ModuleD is initialized", Category.Info, Priority.Medium);
        }
    }
}
