using System;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Modules.Wpf;

namespace ModuleE.Wpf
{
    public class ModuleE : IModule
    {
        readonly ILoggerFacade _logger;
        readonly IModuleTracker _moduleTracker;

        public ModuleE(ILoggerFacade logger, IModuleTracker moduleTracker)
        {
            if( logger==null )
                throw new ArgumentNullException("logger");
            _logger = logger;

            if(moduleTracker==null)
                throw new ArgumentNullException("moduleTracker");
            _moduleTracker = moduleTracker;
            _moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleE);
        }

        public void Initialize()
        {
            _logger.Log("ModuleE is initializing...", Category.Info, Priority.Medium);
            _moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleE);
            _logger.Log("ModuleE is initialized", Category.Info, Priority.Medium);
        }
    }
}
