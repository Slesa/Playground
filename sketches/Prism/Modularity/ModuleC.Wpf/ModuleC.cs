using System;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Modules.Wpf;

namespace ModuleC.Wpf
{
    public class ModuleC : IModule
    {
        readonly ILoggerFacade _logger;
        readonly IModuleTracker _moduleTracker;

        public ModuleC(ILoggerFacade logger, IModuleTracker moduleTracker)
        {
            if( logger==null )
                throw new ArgumentNullException("logger");
            _logger = logger;

            if(moduleTracker==null)
                throw new ArgumentNullException("moduleTracker");
            _moduleTracker = moduleTracker;
            _moduleTracker.RecordModuleConstructed(WellKnownModuleNames.ModuleC);
        }

        public void Initialize()
        {
            _logger.Log("ModuleC is initializing...", Category.Info, Priority.Medium);
            _moduleTracker.RecordModuleInitialized(WellKnownModuleNames.ModuleC);
            _logger.Log("ModuleC is initialized", Category.Info, Priority.Medium);
        }
    }
}
