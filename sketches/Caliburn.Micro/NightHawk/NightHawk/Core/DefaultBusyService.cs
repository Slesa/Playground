using System;
using System.Collections.Generic;
using Caliburn.Micro;

namespace NightHawk.Core
{
    public class DefaultBusyService : IBusyService
    {
        static readonly ILog _log = LogManager.GetLog(typeof (DefaultBusyService));
        
        static string BusyIndicatorName = "busyIndicator";

        private class BusyInfo
        {
            public UIElement BusyIndicator { get; set; }
            public object BusyViewModel { get; set; }
        }

        readonly Dictionary<object,BusyInfo> _loaders = new Dictionary<object, BusyInfo>();
        readonly object _lockObject = new object();
        readonly object _defaultKey = new object();

        readonly IWindowManager _windowManager;

        public void MarkAsBusy(object sourceViewModel, object busyViewModel)
        {
            throw new NotImplementedException();
        }

        public void MarkAsNotBusy(object sourceViewModel)
        {
            throw new NotImplementedException();
        }

        static FrameworkElement GetView(object viewModel)
        {
            var viewAware = viewModel as IViewAware;
            if (viewAware == null)
                return null;
            return viewAware.GetView() as FrameworkElement;
        }
    }
}