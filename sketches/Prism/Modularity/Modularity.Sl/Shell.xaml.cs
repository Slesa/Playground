using Modules.Wpf;
using System;
using System.Globalization;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using System.Windows;

namespace Modularity.Wpf
{
    public partial class Shell : UserControl
    {
        readonly IModuleTracker _moduleTracker;
        readonly IModuleManager _moduleManager;
        readonly CallbackLogger _logger;

        public Shell(IModuleManager moduleManager, IModuleTracker moduleTracker, CallbackLogger logger)
        {
            if (moduleManager == null)
                throw new ArgumentNullException("moduleManager");
            _moduleManager = moduleManager;

            if (moduleTracker == null)
                throw new ArgumentNullException("moduleTracker");
            _moduleTracker = moduleTracker;

            if (logger == null)
                throw new ArgumentNullException("logger");
            _logger = logger;

            DataContext = _moduleTracker;

            _moduleManager.LoadModuleCompleted += PageLoadModuleCompleted;
            _moduleManager.ModuleDownloadProgressChanged += PageModuleDownloadProgressChanged;

            InitializeComponent();            
        }

        public void Log(string message, Category category, Priority priority)
        {
            TraceTextBox.Text += string.Format(CultureInfo.CurrentUICulture, "[{0}][{1}] {2}\r\n", category, priority, message);
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

        void PageLoaded(object sender, RoutedEventArgs e)
        {
            _logger.Callback = Log;
            _logger.ReplaySavedLogs();
        }        

        void PageModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            _moduleTracker.RecordModuleDownloading(e.ModuleInfo.ModuleName, e.BytesReceived, e.TotalBytesToReceive);
        }

        void PageLoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            _moduleTracker.RecordModuleLoaded(e.ModuleInfo.ModuleName);
        }             
    }
}
