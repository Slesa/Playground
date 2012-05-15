using System;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Modules.Wpf;

namespace ModuleF.Wpf
{
    public class ModuleF : IModule
    {
        readonly ILoggerFacade _logger;
        readonly IModuleTracker _moduleTracker;

        public ModuleF(ILoggerFacade logger, IModuleTracker moduleTracker)
        {
            if( logger==null )
                throw new ArgumentNullException("logger");
            _logger = logger;

            if(moduleTracker==null)
                throw new ArgumentNullException("moduleTracker");
            _moduleTracker = moduleTracker;
            _moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleF);
        }

        public void Initialize()
        {
            _logger.Log("ModuleF is initializing...", Category.Info, Priority.Medium);
            _moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleF);
            _logger.Log("ModuleF is initialized", Category.Info, Priority.Medium);
        }
    }
}
