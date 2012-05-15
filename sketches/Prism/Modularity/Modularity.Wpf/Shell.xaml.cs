using System;
using System.Globalization;
using System.Windows;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Modules.Wpf;

namespace Modularity.Wpf
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell
    {
        readonly IModuleManager _moduleManager;
        readonly IModuleTracker _moduleTracker;
        readonly CallbackLogger _logger;

        public Shell(IModuleManager moduleManager, IModuleTracker moduleTracker, CallbackLogger logger)
        {
            if( moduleManager==null ) 
                throw new ArgumentNullException("moduleManager");
            _moduleManager = moduleManager;

            if( moduleTracker==null )
                throw new ArgumentNullException("moduleTracker");
            _moduleTracker = moduleTracker;

            if( logger==null )
                throw new ArgumentNullException("logger");
            _logger = logger;

            InitializeComponent();
        }

        public void Log(string message, Category category, Priority priority)
        {
            TraceTextBox.AppendText(string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2}\r\n", category, priority, message));
        }

        void ModuleB_RequestModuleLoad(object sender, EventArgs e)
        {
            _moduleManager.LoadModule(WellKnownModuleNames.ModuleB);
        }

        void ModuleC_RequestModuleLoad(object sender, EventArgs e)
        {
            _moduleManager.LoadModule(WellKnownModuleNames.ModuleC);
        }

        void ModuleE_RequestModuleLoad(object sender, EventArgs e)
        {
            _moduleManager.LoadModule(WellKnownModuleNames.ModuleE);
        }

        void ModuleF_RequestModuleLoad(object sender, EventArgs e)
        {
            _moduleManager.LoadModule(WellKnownModuleNames.ModuleF);
        }

        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = _moduleTracker;

            _logger.Callback = Log;
            _logger.ReplaySavedLogs();

            _moduleManager.LoadModuleCompleted += WindowLoadModuleCompleted;
            _moduleManager.ModuleDownloadProgressChanged += WindowDownloadProgressChanged;
        }

        void WindowDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            _moduleTracker.RecordModuleDownloading(e.ModuleInfo.ModuleName, e.BytesReceived, e.TotalBytesToReceive);
        }

        void WindowLoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            _moduleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
        }
    }
}
