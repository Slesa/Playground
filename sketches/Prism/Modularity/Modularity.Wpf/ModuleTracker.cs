using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Modularity.Wpf.Resources;
using Modularity.Wpf.Values;
using Modules.Wpf;

namespace Modularity.Wpf
{
    public class ModuleTracker : IModuleTracker
    {
        readonly ILoggerFacade _logger;
        IList<ModuleTrackingState> _moduleStates = new List<ModuleTrackingState>();

        public ModuleTracker(ILoggerFacade logger)
        {
            if( logger==null )
                throw new ArgumentNullException("logger");
            _logger = logger;

            InitializeModules();
        }

        public ModuleTrackingState ModuleATrackingState
        {
            get { return GetModuleTrackingState(WellKnownModuleNames.ModuleA); }
        }

        public ModuleTrackingState ModuleBTrackingState
        {
            get { return GetModuleTrackingState(WellKnownModuleNames.ModuleB); }
        }

        public ModuleTrackingState ModuleCTrackingState
        {
            get { return GetModuleTrackingState(WellKnownModuleNames.ModuleC); }
        }

        public ModuleTrackingState ModuleDTrackingState
        {
            get { return GetModuleTrackingState(WellKnownModuleNames.ModuleD); }
        }

        public ModuleTrackingState ModuleETrackingState
        {
            get { return GetModuleTrackingState(WellKnownModuleNames.ModuleE); }
        }

        public ModuleTrackingState ModuleFTrackingState
        {
            get { return GetModuleTrackingState(WellKnownModuleNames.ModuleF); }
        }

        public void RecordModuleDownloading(string moduleName, long bytesReceived, long totalBytes)
        {
            var trackingState = GetModuleTrackingState(moduleName);
            if (trackingState != null)
            {
                trackingState.BytesReceived = bytesReceived;
                trackingState.TotalBytesToRecevice = totalBytes;
                trackingState.ModuleInitializationStatus = bytesReceived < totalBytes 
                    ? ModuleInitializationStatus.Downloading 
                    : ModuleInitializationStatus.Downloaded;
            }

            _logger.Log(string.Format(CultureInfo.CurrentCulture, Strings.ModuleLoadingProcess, moduleName, bytesReceived, totalBytes), Category.Debug, Priority.Low);
        }

        public void RecordModuleLoaded(string moduleName)
        {
            _logger.Log(string.Format(CultureInfo.CurrentCulture, Strings.ModuleLoaded, moduleName), Category.Debug, Priority.Low);
        }

        public void RecordModuleConstructed(string moduleName)
        {
            var trackingState = GetModuleTrackingState(moduleName);
            if (trackingState != null)
                trackingState.ModuleInitializationStatus = ModuleInitializationStatus.Constructed;

            _logger.Log(string.Format(CultureInfo.CurrentCulture, Strings.ModuleConstructed, moduleName), Category.Debug, Priority.Low);
        }

        public void RecordModuleInitialized(string moduleName)
        {
            var trackingState = GetModuleTrackingState(moduleName);
            if (trackingState != null)
                trackingState.ModuleInitializationStatus = ModuleInitializationStatus.Initialized;

            _logger.Log(string.Format(CultureInfo.CurrentCulture, Strings.ModuleInitialized, moduleName), Category.Debug, Priority.Low);
        }

        ModuleTrackingState GetModuleTrackingState(string moduleName)
        {
            var query = from s in _moduleStates where s.ModuleName.Equals(moduleName) select s;
            var state = query.FirstOrDefault();
            return state;
        }

        void InitializeModules()
        {
            foreach (var trackingState in GetAllTrackingStates())
                _moduleStates.Add(trackingState);
        }

        IEnumerable<ModuleTrackingState> GetAllTrackingStates()
        {
            yield return new ModuleTrackingState
                {
                    ModuleName = WellKnownModuleNames.ModuleA,
                    ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                    ExpectedInitializationMode = InitializationMode.WhenAvailable,
                    ExpectedDownloadTiming = DownloadTiming.WithApplication,
                    ConfiguredDependencies = WellKnownModuleNames.ModuleD,
                };

            yield return new ModuleTrackingState
                {
                    ModuleName = WellKnownModuleNames.ModuleB,
                    ExpectedDiscoveryMethod = DiscoveryMethod.DirectorySweep,
                    ExpectedInitializationMode = InitializationMode.OnDemand,
                    ExpectedDownloadTiming = DownloadTiming.InBackground,
                };

            yield return new ModuleTrackingState
                {
                    ModuleName = WellKnownModuleNames.ModuleC,
                    ExpectedDiscoveryMethod = DiscoveryMethod.ApplicationReference,
                    ExpectedInitializationMode = InitializationMode.OnDemand,
                    ExpectedDownloadTiming = DownloadTiming.WithApplication,
                };

            yield return new ModuleTrackingState
                {
                    ModuleName = WellKnownModuleNames.ModuleD,
                    ExpectedDiscoveryMethod = DiscoveryMethod.DirectorySweep,
                    ExpectedInitializationMode = InitializationMode.WhenAvailable,
                    ExpectedDownloadTiming = DownloadTiming.InBackground,
                };

            yield return new ModuleTrackingState
                {
                    ModuleName = WellKnownModuleNames.ModuleE,
                    ExpectedDiscoveryMethod = DiscoveryMethod.ConfigurationManifest,
                    ExpectedInitializationMode = InitializationMode.OnDemand,
                    ExpectedDownloadTiming = DownloadTiming.InBackground,
                };

            yield return new ModuleTrackingState
                {
                    ModuleName = WellKnownModuleNames.ModuleF,
                    ExpectedDiscoveryMethod = DiscoveryMethod.ConfigurationManifest,
                    ExpectedInitializationMode = InitializationMode.OnDemand,
                    ExpectedDownloadTiming = DownloadTiming.InBackground,
                    ConfiguredDependencies = WellKnownModuleNames.ModuleE,
                };
        }
    }
}