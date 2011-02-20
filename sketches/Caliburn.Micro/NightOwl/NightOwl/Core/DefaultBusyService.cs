using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using Caliburn.Micro;

namespace NightOwl.Core
{
    public class DefaultBusyService : IBusyService
    {
        static readonly ILog Log = LogManager.GetLog(typeof(DefaultBusyService));

        public static string BusyIndicatorName = "busyIndicator";

        class BusyInfo
        {
            public UIElement BusyIndicator { get; set; }
            public object BusyViewModel { get; set; }
        }

        readonly Dictionary<object, BusyInfo> _loaders = new Dictionary<object, BusyInfo>();
        readonly object _lockObject = new object();
        readonly object _defaultKey = new object();

        readonly IWindowManager _windowManager;

        public DefaultBusyService(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }


        public void MarkAsBusy(object sourceViewModel, object busyViewModel)
        {
            sourceViewModel = sourceViewModel ?? _defaultKey;

            if (_loaders.ContainsKey(sourceViewModel))
            {
                var info = _loaders[sourceViewModel];
                info.BusyViewModel = busyViewModel;
                UpdateLoader(info);
            }
            else
            {
                var busyIndicator = TryFindBusyIndicator(sourceViewModel);

                if (busyIndicator == null)
                {
#if WP7
                    var ex = new CaliburnException("No busy indicator with name '" + BusyIndicatorName + "' was found in the UI hierarchy.");
                    Log.Error(ex);
                    throw ex;
#else
                    var notifier = busyViewModel as IActivate;
                    if (notifier == null)
                        return;

                    notifier.Activated += (o, e) =>
                    {
                        if (e.WasInitialized)
                        {
                            var info = new BusyInfo { BusyViewModel = busyViewModel };
                            _loaders[sourceViewModel] = info;
                            UpdateLoader(info);
                        }
                    };

                    Log.Warn("No busy indicator with name '" + BusyIndicatorName + "' was found in the UI hierarchy. Using modal.");
                    _windowManager.ShowDialog(busyViewModel);
#endif
                }
                else
                {
                    var info = new BusyInfo { BusyIndicator = busyIndicator, BusyViewModel = busyViewModel };
                    _loaders[sourceViewModel] = info;
                    ToggleBusyIndicator(info, true);
                    UpdateLoader(info);
                }
            }
        }

        public void MarkAsNotBusy(object sourceViewModel)
        {
            sourceViewModel = sourceViewModel ?? _defaultKey;

            if (!_loaders.ContainsKey(sourceViewModel)) return;

            var info = _loaders[sourceViewModel];

            lock (_lockObject)
            {
                _loaders.Remove(sourceViewModel);
                ToggleBusyIndicator(info, false);
            }
        }

        static void UpdateLoader(BusyInfo info)
        {
            if (info.BusyViewModel == null || info.BusyIndicator == null)
                return;

            var indicatorType = info.BusyIndicator.GetType();
            var content = indicatorType.GetProperty("BusyContent");

            if (content == null)
            {
                var contentProperty = indicatorType.GetAttributes<ContentPropertyAttribute>(true)
                    .FirstOrDefault();

                if (contentProperty == null)
                    return;

                content = indicatorType.GetProperty(contentProperty.Name);
            }
            content.SetValue(info.BusyIndicator, info.BusyViewModel, null);
        }

        static void ToggleBusyIndicator(BusyInfo info, bool isBusy)
        {
            if (info.BusyIndicator != null)
            {
                var busyProperty = info.BusyIndicator.GetType().GetProperty("IsBusy");
                if (busyProperty != null)
                    busyProperty.SetValue(info.BusyIndicator, isBusy, null);
                else info.BusyIndicator.Visibility = isBusy ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (!isBusy)
            {
                var close = info.BusyViewModel.GetType().GetMethod("Close", Type.EmptyTypes);
                if (close != null)
                    close.Invoke(info.BusyViewModel, null);
            }
        }

        static UIElement TryFindBusyIndicator(object viewModel)
        {
            var view = GetView(viewModel);
            if (view == null)
            {
                Log.Warn("Could not find view for {0}.", viewModel);
                return null;
            }

            UIElement busyIndicator = null;

            while (view != null && busyIndicator == null)
            {
                busyIndicator = view.FindName(BusyIndicatorName) as UIElement;
                view = VisualTreeHelper.GetParent(view) as FrameworkElement;
            }

            return busyIndicator;
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